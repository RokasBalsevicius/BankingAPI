namespace BankingApi.DTOs;


public class CustomerTransactionsHistoryResultDto{
    public int CustomerId { get; set; }
    public IEnumerable<AccountTransactionsDto> Accounts { get; set; }
}


public class AccountTransactionsDto
{
    public int AccountId { get; set; }
    public IEnumerable<TransactionResponseDto> Transactions { get; set; }
}

