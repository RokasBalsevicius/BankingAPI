using BankingApi.Models;

namespace BankingApi.Database;

public class BankingDb
{
    public List<Customer> Customers { get; set; } = new();
    public List<Transaction> Transactions { get; set; } = new();
}