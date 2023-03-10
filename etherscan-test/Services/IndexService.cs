using System.Linq;
using System.Text;
using System.Text.Json;
using etherscan_test.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace etherscan_test.Services
{
	public class IndexService
	{
		private readonly HttpClient _client;
		private readonly ILogger<IndexService> _logger;
		private readonly ApplicationDbContext _dbContext;

		private string _apiKey;
		private string _baseUrl;

		public IndexService(ILogger<IndexService> logger, IConfigurationRoot configuration, ApplicationDbContext dbContext)
		{
			_client = new HttpClient();
            _logger = logger;
            _dbContext = dbContext;

            _apiKey = configuration.GetSection("Alchemy").GetValue<string>("ApiKey");
			_baseUrl = $"https://eth-mainnet.g.alchemy.com/v2/{_apiKey}";

        }

		private async Task<T> ParseApiResponse<T>(HttpResponseMessage response)
		{
			string contentString = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<T>(contentString);
		}

		public async Task<BlockResponse> GetBlockByNumber(int number)
		{
			var body = JsonHelper.BuildRequestBody("eth_getBlockByNumber", $"[\"{NumberHelper.DecimalToHex(number)}\", false]");
            _logger.LogInformation($"Calling eth_getBlockByNumber for number = {number} with body = {body}");
            var bodyContent = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_baseUrl}", bodyContent);
			var responseObject = await ParseApiResponse<Root<BlockResponse>>(response);
			_logger.LogInformation($"Get Block By Number API result {StringHelper.PrettyPrintObject(responseObject)}");

            return responseObject.result;
		}

		public async Task<string> GetTransactionCountByNumber(int number)
        {
            var body = JsonHelper.BuildRequestBody("eth_getBlockTransactionCountByNumber", $"\"{NumberHelper.DecimalToHex(number)}\"");
            _logger.LogInformation($"Calling eth_getBlockTransactionCountByNumber for number = {number} with body = {body}");
            var bodyContent = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_baseUrl}", bodyContent);
			var responseObject = await ParseApiResponse<Root<string>>(response);
            _logger.LogInformation($"Get Transaction Count By Number API result {StringHelper.PrettyPrintObject(responseObject)}");

            return responseObject.result;
        }

		public async Task<TransactionResponse> GetTransactionByBlockNumberAndIndex(int number, int index)
        {
            var body = JsonHelper.BuildRequestBody("eth_getTransactionByBlockNumberAndIndex", $"[\"{NumberHelper.DecimalToHex(number)}\", \"{NumberHelper.DecimalToHex(index)}\"]");
            _logger.LogInformation($"Calling eth_getBlockTransactionCountByNumber for number = {number} with body = {body}");
            var bodyContent = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_baseUrl}", bodyContent);
            var responseObject = await ParseApiResponse<Root<TransactionResponse>>(response);
            _logger.LogInformation($"Get Transaction Count By Number API result {StringHelper.PrettyPrintObject(responseObject)}");

            return responseObject.result;
        }

		public async Task IndexBlocks()
        {
            // First let's just retrieve all the blocks we've already saved
            var allLocalBlocks = _dbContext.Blocks.ToList();
            // go through all the numbers
            for (var i = 1210001; i <= 12100500; i++)
            {
                if (!allLocalBlocks.Any((block) => block.BlockNumber == i))
                {
                    var trx = _dbContext.Database.BeginTransaction();
                    try
                    {
                        var block = await GetBlockByNumber(i);
                        var transactions = new List<TransactionResponse>();

                        var transactionCountHex = await GetTransactionCountByNumber(i);

                        var listOfTasks = new List<Task<TransactionResponse>>();
                        for (var j = 0; j < NumberHelper.HexToDecimal(transactionCountHex); j++)
                        {
                            listOfTasks.Add(GetTransactionByBlockNumberAndIndex(i, j));
                        }
                        transactions.AddRange(await Task.WhenAll(listOfTasks));

                        var localBlock = new Block(block);
                        localBlock.Transactions = transactions.Select((transaction) => new Transaction(transaction)).ToList();

                        _dbContext.Blocks.Add(localBlock);

                        _dbContext.SaveChanges();

                        trx.Commit();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Failed to index block with number {i}: \n{ex.Message}\n{ex.StackTrace}");
                        trx.Rollback();
                    }
                    finally
                    {
                        trx.Dispose();
                    }
                    // Maybe we can update the information instead of skipping it?
                    _logger.LogInformation($"Skipping block with number {i} as it's already in the database");
                }
            }
		}
	}
}

