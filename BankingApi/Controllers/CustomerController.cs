using BankingApi.DTOs;
using BankingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingApi.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomersController(ICustomerService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult CreateCustomer(CreateCustomerDto dto)
    {
        var customer = _service.CreateCustomer(dto);
        return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAllCustomers());
    }

    [HttpGet("{id}")]
    public IActionResult GetCustomer(int id)
    {
        var customer = _service.GetCustomer(id);
        if (customer == null)
        {
            throw new KeyNotFoundException("Customer not found.");
        }
        return Ok(customer);
    }
}