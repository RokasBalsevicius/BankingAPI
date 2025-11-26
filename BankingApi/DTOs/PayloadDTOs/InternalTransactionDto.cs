namespace BankingApi.DTOs;

public class InternalTransactionDto
{
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = "";
}