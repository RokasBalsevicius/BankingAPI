using BankingApi.DTOs;
using BankingApi.Models;
using BankingApi.SQLiteDatabase;
using Microsoft.EntityFrameworkCore;

namespace BankingApi.Repositories;

public class CustomerRepository : ICustomerRepository
{
    public readonly BankingContext _context;

    public CustomerRepository(BankingContext context)
    {
        _context = context;
    }

    public Customer Add(Customer customer)
    {
        _context.Customers.Add(customer);
        _context.SaveChanges();
        return customer;
    }

    public IEnumerable<CustomerResponseDto> GetAll()
    {
        return _context.Customers.Include(c => c.Accounts)
            .Select(c => new CustomerResponseDto
            {
                Id = c.Id,
                FullName = c.FullName,
                PersonalNumber = c.PersonalNumber,
                Accounts = c.Accounts.Select(a => new AccountResponseDto
                {
                    Id = a.Id,
                    Type = a.Type,
                    Balance = a.Balance
                }).ToList()
            }).ToList();
    }

    public Customer? GetByIdEntity(int id)
{
    return _context.Customers
        .Include(c => c.Accounts)
        .ThenInclude(a => a.Transactions)
        .FirstOrDefault(c => c.Id == id);
}

    public CustomerResponseDto? GetById(int id)
    {
        return _context.Customers
            .Where(c => c.Id == id)
            .Select(c => new CustomerResponseDto
            {
                Id = c.Id,
                FullName = c.FullName,
                PersonalNumber = c.PersonalNumber
             })
            .FirstOrDefault();
    }
    
}