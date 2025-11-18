using BankingApi.DTOs;
using BankingApi.Models;
using BankingApi.Repositories;

namespace BankingApi.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repo;

    public CustomerService(ICustomerRepository repo)
    {
        _repo = repo;
    }

    public Customer CreateCustomer(CreateCustomerDto dto)
    {
        var customer = new Customer
        {
            FullName = dto.FullName,
            PersonalNumber = dto.PersonalNumber
        };
        return _repo.Add(customer);
    }

    public IEnumerable<Customer> GetAllCustomers()
    {
        return _repo.GetAll();
    }

    public Customer? GetCustomer(int id)
    {
        return _repo.GetById(id);
    }
}