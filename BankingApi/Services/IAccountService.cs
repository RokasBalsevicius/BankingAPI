using BankingApi.DTOs;
using BankingApi.Models;

namespace BankingApi.Services;

public interface IAccountService
{
    Account CreateAccount(int customerId, CreateAccountDto dto);
    IEnumerable<Account> GetAccounts(int customerId);
    Account? GetAccount(int customerId, int accountId);
}