namespace BankingApi.DTOs;

public class WithdrawalDto
{
    public decimal Amount { get; set; }
    public string Description { get; set; } = "";
}