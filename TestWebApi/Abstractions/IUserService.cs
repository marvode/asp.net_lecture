using TestWebApi.Model;

namespace TestWebApi.Abstractions;

public interface IUserService
{
    public User? Register(string name, string email, string password);
}