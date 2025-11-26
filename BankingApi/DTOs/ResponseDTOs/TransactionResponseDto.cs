namespace BankingApi.DTOs;

public class TransactionResponseDto
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = "";
    public string Type { get; set; } = "";
    public int AccountId { get; set; }
}
