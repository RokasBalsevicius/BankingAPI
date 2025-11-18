using BankingApi.Database;
using BankingApi.Models;

namespace BankingApi.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly BankingDb _db;

    public AccountRepository(BankingDb db)
    {
        _db = db;
    }

    public Account AddAccount(int customerId, Account account)
    {
        var customer = _db.Customers.FirstOrDefault(c => c.Id == customerId);
        if (customer == null)
        {
            throw new Exception("Customer not found.");
        }
        account.Id = customer.Accounts.Any() ? customer.Accounts.Max(a => a.Id) + 1 : 1;
        customer.Accounts.Add(account);
        return account;
    }

    public Account? GetAccount(int customerId, int accountId)
    {
        return _db.Customers.
            FirstOrDefault(c => c.Id == customerId)?
            .Accounts.FirstOrDefault(t => t.Id == accountId);

    }

    public IEnumerable<Account> GetAccounts(int customerId)
    {
        return _db.Customers.FirstOrDefault(c => c.Id == customerId)?.Accounts ?? Enumerable.Empty<Account>();
    }

    public void Update(Account account)
    {
        
    }
}
