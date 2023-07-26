namespace TestWebApi.Model;

public class Entity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
}