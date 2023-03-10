
using etherscan_test;
using Microsoft.Extensions.Logging;
using System.Linq;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using etherscan_test.Helpers;
using etherscan_test.Services;

class Program
{
    static void Main(string[] args)
    {
        IServiceCollection services = new ServiceCollection();
        Startup startup = new Startup();
        startup.ConfigurationServices(services);
        services.AddLogging(logging =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("Logs/Api_Log.txt")
                .CreateLogger();

            logging
                .ClearProviders()
                .AddConsole()
                .AddSerilog(Log.Logger);
        });


        IServiceProvider serviceProvider = services.BuildServiceProvider();
        IndexService indexService = serviceProvider.GetService<IndexService>();
        var logger = serviceProvider.GetService<ILogger<Program>>();

        logger.LogInformation($"Calling index service's indexBlocks function");

        indexService.IndexBlocks().Wait();
    }
}