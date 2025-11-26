using BankingApi.DTOs;
using BankingApi.Models;

namespace BankingApi.Services;

public interface ICustomerService
{
    Customer CreateCustomer(CreateCustomerDto dto);
    IEnumerable<CustomerResponseDto> GetAllCustomers();
    CustomerResponseDto? GetCustomer(int id);
}