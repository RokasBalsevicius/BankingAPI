using BankingApi.Models;
using BankingApi.DTOs;

namespace BankingApi.Repositories;

public interface IAccountRepository
{
    Account AddAccount(int customerId, Account account);
    AccountResponseDto? GetAccount(int customerId, int accountId);
    IEnumerable<AccountResponseDto> GetAccounts(int customerId);
    void Update(Account account);
    Account? GetAccountEntity(int customerId, int accountId);
}