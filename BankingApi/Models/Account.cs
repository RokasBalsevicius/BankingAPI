namespace BankingApi.Models;

public class Account
{
    public int Id { get; set; }
    public string Type { get; set; } = "";
    public decimal Balance { get; set; } = 0;
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public List<Transaction> Transactions { get; set; } = new();
}