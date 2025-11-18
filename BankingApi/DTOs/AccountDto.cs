//*This class is for response*/ 
namespace BankingApi.DTOs;

public class AccountDto
{
    public int Id { get; set; }
    public string Type { get; set; } = "";
    public decimal Balance { get; set; }
}