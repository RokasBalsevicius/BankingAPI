using System.ComponentModel.DataAnnotations;

namespace BankingApi.DTOs;

public class DepositDto
{
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }
    public string Description { get; set; } = "";
}