namespace BankingApi.DTOs;

public class CustomerResponseDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string PersonalNumber { get; set; } = "";
    public List<AccountResponseDto> Accounts { get; set; } = new();
}