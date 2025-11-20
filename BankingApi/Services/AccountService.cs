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
            throw new Exception("Customer not found");
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
        var account = _repo.GetAccount(customerId, accountId) ?? throw new Exception("Account not found.");
        if (dto.Amount <= 0)
        {
            throw new Exception("Deposit amount must be positive");
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
        var account = _repo.GetAccount(customerId, accountId) ?? throw new Exception("Account not found.");

        if (account.Balance <= 0)
        {
            throw new Exception($"No available balance in the '{account.Type}' account");
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
}