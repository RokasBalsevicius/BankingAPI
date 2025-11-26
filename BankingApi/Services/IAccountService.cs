using BankingApi.DTOs;

namespace BankingApi.Services;

public interface IAccountService
{
    AccountResponseDto CreateAccount(int customerId, CreateAccountDto dto);
    IEnumerable<AccountResponseDto> GetAccounts(int customerId);
    AccountResponseDto? GetAccount(int customerId, int accountId);
    TransactionResponseDto Deposit(int customerId, int accountId, DepositDto dto);
    TransactionResponseDto Withdrawal(int customerId, int accountId, WithdrawalDto dto);
    InternalTransferResultDto InternalTransfer(int customerId, InternalTransactionDto dto);
    CustomerTransactionsHistoryResultDto GetTransactionsHistory(int customerId);
}