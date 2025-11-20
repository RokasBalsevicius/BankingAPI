using BankingApi.Models;

namespace BankingApi.DTOs;

public class InternalTransferResultDto
{
    public Transaction DebitTransaction { get; set; }
    public Transaction CreditTransaction{ get; set; }
}