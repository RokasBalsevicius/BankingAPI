using BankingApi.Models;

namespace BankingApi.Repositories;

public interface ICustomerRepository
{
    Customer? GetById(int id);
    IEnumerable<Customer> GetAll();
    Customer Add(Customer customer);
}