namespace TestWebApi.Model;

public class User: Entity
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public byte[] Salt { get; set; }
    public string Role { get; set; }
}