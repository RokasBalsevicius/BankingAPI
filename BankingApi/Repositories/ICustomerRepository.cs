using BankingApi.DTOs;
using BankingApi.Models;

namespace BankingApi.Repositories;

public interface ICustomerRepository
{
    Customer? GetByIdEntity(int id);
    CustomerResponseDto? GetById(int id);
    IEnumerable<CustomerResponseDto> GetAll();
    Customer Add(Customer customer);
}