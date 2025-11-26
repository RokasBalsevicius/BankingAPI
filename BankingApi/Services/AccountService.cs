using BankingApi.DTOs;
using BankingApi.Models;
using BankingApi.Repositories;

namespace BankingApi.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _repo;
    private readonly ICustomerRepository _customerRepo;

    public AccountService(IAccountRepository repo, ICustomerRepository customerRepo)
    {
        _repo = repo;
        _customerRepo = customerRepo;
    }

    public AccountResponseDto CreateAccount(int customerId, CreateAccountDto dto)
    {
        var customer = _customerRepo.GetById(customerId);
        if (customer == null)
        {
            throw new KeyNotFoundException("Customer not found");
        }
        var account = _repo.AddAccount(customerId, new Account
        {
            Type = dto.Type,
            Balance = 0
        });

        return new AccountResponseDto
        {
            Id = account.Id,
            Type = account.Type,
            Balance = account.Balance
        };
    }

    public AccountResponseDto? GetAccount(int customerId, int accountId)
    {
        return _repo.GetAccount(customerId, accountId);
    }

    public IEnumerable<AccountResponseDto> GetAccounts(int customerId)
    {
        return _repo.GetAccounts(customerId);
    }

    public TransactionResponseDto Deposit(int customerId, int accountId, DepositDto dto)
    {
        var account = _repo.GetAccountEntity(customerId, accountId) ?? throw new KeyNotFoundException("Account not found.");
        if (dto.Amount <= 0)
        {
            throw new ArgumentException("Deposit amount must be positive");
        }
        if (dto.Amount > 10_000)
        {
            throw new ArgumentException("Deposit limit exceeded");
        }

        account.Balance += dto.Amount;

        var transaction = new Transaction
        {
            Amount = dto.Amount,
            Description = dto.Description,
            Type = "Deposit",
            AccountId = account.Id
        };


        account.Transactions.Add(transaction);
        _repo.Update(account);
        return new TransactionResponseDto
        {
            Id = transaction.Id,
            Timestamp = transaction.Timestamp,
            Amount = transaction.Amount,
            Description = transaction.Description,
            Type = transaction.Type,
            AccountId = transaction.AccountId
        };
    }

    public TransactionResponseDto Withdrawal(int customerId, int accountId, WithdrawalDto dto)
    {
        var account = _repo.GetAccountEntity(customerId, accountId) ?? throw new KeyNotFoundException("Account not found.");

        if (account.Balance <= dto.Amount)
        {
            throw new ArgumentException($"Insufficient balance.");
        }
        if (dto.Amount > 7_000)
        {
            throw new ArgumentException("Withdrawal amount exceeded");
        }

        account.Balance -= dto.Amount;

        var transaction = new Transaction
        {
            Amount = dto.Amount,
            Description = dto.Description,
            Type = "Withdrawal",
            AccountId = account.Id
        };

        account.Transactions.Add(transaction);
        _repo.Update(account);
        return new TransactionResponseDto
        {
            Id = transaction.Id,
            Timestamp = transaction.Timestamp,
            Amount = transaction.Amount,
            Description = transaction.Description,
            Type = transaction.Type,
            AccountId = transaction.AccountId
        };
    }

    public InternalTransferResultDto InternalTransfer(int customerId, InternalTransactionDto dto)
    {
        var accountToDebit = _repo.GetAccountEntity(customerId, dto.FromAccountId) ?? throw new KeyNotFoundException("Account not found");
        var accountToCredit = _repo.GetAccountEntity(customerId, dto.ToAccountId) ?? throw new KeyNotFoundException("Account not found");
        if (accountToDebit.Balance < dto.Amount)
        {
            throw new ArgumentException("Insufficient balance in account to debit");
        }
        if (dto.FromAccountId == dto.ToAccountId)
        {
            throw new ArgumentException("Cannot transfer to the same account");
        }

        accountToDebit.Balance -= dto.Amount;
        accountToCredit.Balance += dto.Amount;

        var debitTransaction = new Transaction
        {
            Amount = dto.Amount,
            Description = dto.Description,
            Type = "Withdrawal",
            AccountId = accountToDebit.Id
        };

        var creditTransaction = new Transaction
        {
            Amount = dto.Amount,
            Description = dto.Description,
            Type = "Deposit",
            AccountId = accountToCredit.Id
        };

        accountToDebit.Transactions.Add(debitTransaction);
        accountToCredit.Transactions.Add(creditTransaction);
        _repo.Update(accountToDebit);
        _repo.Update(accountToCredit);

        return new InternalTransferResultDto
        {
            DebitTransaction = new TransactionResponseDto
            {
                Id = debitTransaction.Id,
                Timestamp = debitTransaction.Timestamp,
                Amount = debitTransaction.Amount,
                Description = debitTransaction.Description,
                Type = debitTransaction.Type,
                AccountId = debitTransaction.AccountId
            },
            CreditTransaction = new TransactionResponseDto
            {
                Id = creditTransaction.Id,
                Timestamp = creditTransaction.Timestamp,
                Amount = creditTransaction.Amount,
                Description = creditTransaction.Description,
                Type = creditTransaction.Type,
                AccountId = creditTransaction.AccountId
            }
        };
    }

    public CustomerTransactionsHistoryResultDto GetTransactionsHistory(int customerId)
    {
        var customer = _customerRepo.GetByIdEntity(customerId) ?? throw new ArgumentException("Customer not found");
        if (customer.Accounts == null) {
            throw new ArgumentException("Accounts not found");
        }

        return new CustomerTransactionsHistoryResultDto
        {
            CustomerId = customer.Id,
            Accounts = customer.Accounts.Select(a => new AccountTransactionsDto
            {
                AccountId = a.Id,
                Transactions = a.Transactions.Select(t => new TransactionResponseDto
                {
                    Id = t.Id,
                    Timestamp = t.Timestamp,
                    Amount = t.Amount,
                    Description = t.Description,
                    Type = t.Type,
                    AccountId = t.AccountId
                }).ToList()
            }).ToList()
        };
    }
}