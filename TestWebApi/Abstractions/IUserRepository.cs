using TestWebApi.Model;

namespace TestWebApi.Abstractions;

public interface IUserRepository
{
    public User? Register(User user);
}