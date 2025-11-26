namespace BankingApi.DTOs;

public class AccountResponseDto
{
    public int Id { get; set; }
    public string Type { get; set; } = "";
    public decimal Balance { get; set; }
}