namespace BankingApi.Models;

public class Transaction
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public decimal Amount { get; set; }
    public string Description { get; set; } = "";
    public string Type { get; set; } = "";
}