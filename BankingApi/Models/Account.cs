namespace BankingApi.Models;

public class Account
{
    public int Id { get; set; }
    public string Type { get; set; } = "";
    public decimal Balance { get; set; }
    public List<Transaction> Transactions { get; set; } = new();
}