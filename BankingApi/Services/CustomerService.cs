using BankingApi.DTOs;
using BankingApi.Models;
using BankingApi.Repositories;
using BankingApi.SQLiteDatabase;

namespace BankingApi.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repo;
    private readonly BankingContext _context;

    public CustomerService(ICustomerRepository repo, BankingContext context)
    {
        _repo = repo;
        _context = context;
    }

    public Customer CreateCustomer(CreateCustomerDto dto)
    {
        bool exists = _context.Customers.Any(c => c.PersonalNumber == dto.PersonalNumber);
        if (exists)
        {
            throw new ArgumentException("Unexpected error. Contact support");
        }
        var customer = new Customer
        {
            FullName = dto.FullName,
            PersonalNumber = dto.PersonalNumber
        };
        return _repo.Add(customer);
    }

    public IEnumerable<CustomerResponseDto> GetAllCustomers()
    {
        return _repo.GetAll();
    }

    public CustomerResponseDto? GetCustomer(int id)
    {
        return _repo.GetById(id);
    }
}