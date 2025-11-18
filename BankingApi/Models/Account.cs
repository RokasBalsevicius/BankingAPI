namespace BankingApi.Models;

public class Account
{
    public int Id { get; set; }
    public string Type { get; set; } = "";
    public decimal Balance { get; set; } = 0;
    public List<Transaction> Transactions { get; set; } = new();
}