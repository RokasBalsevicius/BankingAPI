using BankingApi.Database;
using BankingApi.Models;

namespace BankingApi.Repositories;

public class CustomerRepository : ICustomerRepository
{
    public readonly BankingDb _db;

    public CustomerRepository(BankingDb db)
    {
        _db = db;
    }

    public Customer Add(Customer customer)
    {
        customer.Id = _db.Customers.Any() ? _db.Customers.Max(c => c.Id) + 1 : 1;
        _db.Customers.Add(customer);
        return customer;
    }

    public IEnumerable<Customer> GetAll()
    {
        return _db.Customers;
    }

    public Customer? GetById(int id)
    {
        return _db.Customers.FirstOrDefault(c => c.Id == id);
    }
    
}