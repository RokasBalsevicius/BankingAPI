namespace BankingApi.DTOs;

public class DepositDto
{
    public decimal Amount { get; set; }
    public string Description { get; set; } = "";
}