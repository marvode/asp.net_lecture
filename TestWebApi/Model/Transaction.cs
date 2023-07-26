namespace TestWebApi.Model;

public class Transaction: Entity
{
    public string AccountId { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; }

    public Account Account { get; set; }
}