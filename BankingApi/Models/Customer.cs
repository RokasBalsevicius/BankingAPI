namespace BankingApi.Models;

public class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string PersonalNumber { get; set; } = "";
    public List<Account> Accounts { get; set; } = new();

}