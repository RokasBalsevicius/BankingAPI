namespace BankingApi.DTOs;

public class InternalTransferResultDto
{
    public TransactionResponseDto DebitTransaction { get; set; }
    public TransactionResponseDto CreditTransaction{ get; set; }
}