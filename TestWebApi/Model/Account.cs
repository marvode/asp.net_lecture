namespace TestWebApi.Model;

public class Account: Entity
{
    public string UserId { get; set; }
    public decimal Balance { get; set; }

    public User User { get; set; }
    public IEnumerable<Transaction> Transactions { get; set; }
}