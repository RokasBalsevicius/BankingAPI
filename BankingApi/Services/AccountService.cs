using BankingApi.DTOs;
using BankingApi.Models;
using BankingApi.Repositories;
using BankingApi.Database;

namespace BankingApi.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _repo;
    private readonly ICustomerRepository _customerRepo;
    private readonly BankingDb _db;

    public AccountService(IAccountRepository repo, ICustomerRepository customerRepo, BankingDb db)
    {
        _repo = repo;
        _customerRepo = customerRepo;
        _db = db;
    }

    public Account CreateAccount(int customerId, CreateAccountDto dto)
    {
        var customer = _customerRepo.GetById(customerId);
        if (customer == null)
        {
            throw new KeyNotFoundException("Customer not found");
        }
        var account = new Account
        {
            Type = dto.Type,
            Balance = 0
        };

        return _repo.AddAccount(customerId, account);
    }

    public Account? GetAccount(int customerId, int accountId)
    {
        return _repo.GetAccount(customerId, accountId);
    }

    public IEnumerable<Account> GetAccounts(int customerId)
    {
        return _repo.GetAccounts(customerId);
    }

    public Transaction Deposit(int customerId, int accountId, DepositDto dto)
    {
        var account = _repo.GetAccount(customerId, accountId) ?? throw new KeyNotFoundException("Account not found.");
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
            Id = _db.Transactions.Any() ? _db.Transactions.Max(t => t.Id) + 1 : 1,
            Amount = dto.Amount,
            Description = dto.Description,
            Type = "Deposit"
        };
        _db.Transactions.Add(transaction);
        account.Transactions.Add(transaction);
        _repo.Update(account);
        return transaction;
    }

    public Transaction Withdrawal(int customerId, int accountId, WithdrawalDto dto)
    {
        var account = _repo.GetAccount(customerId, accountId) ?? throw new KeyNotFoundException("Account not found.");

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
            Id = _db.Transactions.Any() ? _db.Transactions.Max(t => t.Id) + 1 : 1,
            Amount = dto.Amount,
            Description = dto.Description,
            Type = "Withdrawal"
        };

        _db.Transactions.Add(transaction);
        account.Transactions.Add(transaction);
        _repo.Update(account);
        return transaction;
    }

    public InternalTransferResultDto InternalTransfer(int customerId, InternalTransactionDto dto)
    {
        var accountToDebit = _repo.GetAccount(customerId, dto.FromAccountId) ?? throw new KeyNotFoundException("Account not found");
        var accountToCredit = _repo.GetAccount(customerId, dto.ToAccountId) ?? throw new KeyNotFoundException("Account not found");
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

        var baseId = _db.Transactions.Any() ? _db.Transactions.Max(t => t.Id) + 1 : 1;

        var debitTransaction = new Transaction
        {
            Id = baseId,
            Amount = dto.Amount,
            Description = dto.Description,
            Type = "Withdrawal"
        };

        var creditTransaction = new Transaction
        {
            Id = baseId + 1,
            Amount = dto.Amount,
            Description = dto.Description,
            Type = "Deposit"
        };

        _db.Transactions.Add(debitTransaction);
        _db.Transactions.Add(creditTransaction);
        accountToDebit.Transactions.Add(debitTransaction);
        accountToCredit.Transactions.Add(creditTransaction);
        _repo.Update(accountToDebit);
        _repo.Update(accountToCredit);

        return new InternalTransferResultDto
        {
            DebitTransaction = debitTransaction,
            CreditTransaction = creditTransaction
        };
    }

    public CustomerTransactionsHistoryResultDto GetTransactionsHistory(int customerId)
    {
        var customer = _customerRepo.GetById(customerId) ?? throw new ArgumentException("Customer not found");
        if (customer.Accounts == null) {
            throw new ArgumentException("Accounts not found");
        }

        return new CustomerTransactionsHistoryResultDto
        {
            CustomerId = customer.Id,
            Accounts = customer.Accounts.Select(a => new AccountTransactionsDto
            {
                AccountId = a.Id,
                Transactions = a.Transactions
            })
        };
    }
}