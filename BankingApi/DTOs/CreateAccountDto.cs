using System.ComponentModel.DataAnnotations;

namespace BankingApi.DTOs;

public class CreateAccountDto
{
    [Required]
    [RegularExpression("Checking|Savings")]
    public string Type { get; set; } = "";
}