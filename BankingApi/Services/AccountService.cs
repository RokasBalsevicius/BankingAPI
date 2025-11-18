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


}