using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using etherscan_test.Helpers;

public partial class Block
{
    public Block()
    {

    }

    public Block(BlockResponse block)
    {
        BlockNumber = NumberHelper.HexToDecimal(block.number);
        Hash = block.hash;
        ParentHash = block.parentHash;
        Miner = block.miner;
        BlockReward = 2;
        GasLimit = NumberHelper.HexToDecimal(block.gasLimit);
        GasUsed = NumberHelper.HexToDecimal(block.gasUsed);
    }

    public int BlockId { get; set; }
    public int BlockNumber { get; set; }
    public string? Hash { get; set; }
    public string? ParentHash { get; set; }
    public string? Miner { get; set; }
    public decimal BlockReward { get; set; }
    public decimal GasLimit { get; set; }
    public decimal GasUsed { get; set; }

    public IList<Transaction>? Transactions { get; set; }
}

public partial class Transaction
{
    public Transaction()
    {

    }

    public Transaction(TransactionResponse transaction)
    {
        Hash = transaction.hash;
        From = transaction.from;
        To = transaction.to;
        Value = NumberHelper.HexToDecimal(transaction.value);
        Gas = NumberHelper.HexToDecimal(transaction.gas);
        GasPrice = NumberHelper.HexToDecimal(transaction.gasPrice);
        TransactionIndex = NumberHelper.HexToDecimal(transaction.transactionIndex);
    }

    public int TransactionId { get; set; }
    public int BlockId { get; set; }
    public string Hash { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public decimal Value { get; set; }
    public decimal Gas { get; set; }
    public decimal GasPrice { get; set; }
    public int TransactionIndex { get; set; }

    [JsonIgnore]
    public virtual Block Block { get; set; }
}