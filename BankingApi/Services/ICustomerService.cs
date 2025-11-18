using BankingApi.DTOs;
using BankingApi.Models;

namespace BankingApi.Services;

public interface ICustomerService
{
    Customer CreateCustomer(CreateCustomerDto dto);
    IEnumerable<Customer> GetAllCustomers();
    Customer? GetCustomer(int id);
}