using BankingApi.DTOs;
using BankingApi.Models;

namespace BankingApi.Services;

public interface IAccountService
{
    Account CreateAccount(int customerId, CreateAccountDto dto);
    IEnumerable<Account> GetAccounts(int customerId);
    Account? GetAccount(int customerId, int accountId);
    Transaction Deposit(int customerId, int accountId, DepositDto dto);
    Transaction Withdrawal(int customerId, int accountId, WithdrawalDto dto);
    InternalTransferResultDto InternalTransfer(int customerId, InternalTransactionDto dto);
    CustomerTransactionsHistoryResultDto GetTransactionsHistory(int customerId);
}