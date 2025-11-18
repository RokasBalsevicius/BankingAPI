using BankingApi.DTOs;
using BankingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingApi.Controllers;

[ApiController]
[Route("api/customers/{customerId}/accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _service;

    public AccountController(IAccountService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult CreateAccount(int customerId, CreateAccountDto dto)
    {
        try
        {
            var account = _service.CreateAccount(customerId, dto);
            return CreatedAtAction(nameof(GetAccount),
            new { customerId = customerId, accountId = account.Id },
            account);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet]
    public IActionResult GetAccounts(int customerId)
    {
        return Ok(_service.GetAccounts(customerId));
    }

    [HttpGet("{accountId}")]
    public IActionResult GetAccount(int customerId, int accountId)
    {
        var account = _service.GetAccount(customerId, accountId);
        if (account == null)
        {
            return NotFound();
        }
        return Ok(account);
    }

    [HttpPost("{accountId}/deposit")]
    public IActionResult Deposit(int customerId, int accountId, DepositDto dto)
    {
        try
        {
            var transaction = _service.Deposit(customerId, accountId, dto);
            return Ok(transaction);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("{accountId}/withdraw")]
    public IActionResult Withdrawal(int customerId, int accountId, WithdrawalDto dto)
    {
        try
        {
            var transaction = _service.Withdrawal(customerId, accountId, dto);
            return Ok(transaction);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}