using BankingApi.Models;

namespace BankingApi.Repositories;

public interface IAccountRepository
{
    Account AddAccount(int customerId, Account account);
    Account? GetAccount(int customerId, int accountId);
    IEnumerable<Account> GetAccounts(int customerId);
}