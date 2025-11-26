using BankingApi.Models;
using BankingApi.SQLiteDatabase;
using BankingApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BankingApi.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly BankingContext _context;

    public AccountRepository(BankingContext context)
    {
        _context = context;
    }

    public Account AddAccount(int customerId, Account account)
    {
        var customer = _context.Customers.FirstOrDefault(c => c.Id == customerId) ?? throw new ArgumentException("Customer not found");

        account.CustomerId = customerId;
        _context.Accounts.Add(account);
        _context.SaveChanges();
        return account;
    }

    public Account? GetAccountEntity(int customerId, int accountId)
    {
        return _context.Accounts
            .Include(a => a.Transactions)
            .FirstOrDefault(a => a.Id == accountId && a.CustomerId == customerId);
    }

    public AccountResponseDto? GetAccount(int customerId, int accountId)
    {
        var a = GetAccountEntity(customerId, accountId);

        if (a == null) return null;

        return new AccountResponseDto
        {
            Id = a.Id,
            Type = a.Type,
            Balance = a.Balance
        };
    }

    public IEnumerable<AccountResponseDto> GetAccounts(int customerId)
    {
        var accounts = _context.Accounts
            .Where(c => c.CustomerId == customerId)?.ToList() ?? throw new ArgumentException("Accounts not found");

        return accounts.Select(a => new AccountResponseDto
        {
            Id = a.Id,
            Type = a.Type,
            Balance = a.Balance
        }).ToList();
    }

    public void Update(Account account)
    {
        _context.Accounts.Update(account);
        _context.SaveChanges();
    }
}
