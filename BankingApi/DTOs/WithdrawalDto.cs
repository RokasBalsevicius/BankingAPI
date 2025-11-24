using System.ComponentModel.DataAnnotations;

namespace BankingApi.DTOs;

public class WithdrawalDto
{
    [Required]
    [Range(0.1, double.MaxValue)]
    public decimal Amount { get; set; }
    public string Description { get; set; } = "";
}