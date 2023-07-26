using TestWebApi.DataTransferObjects;
using TestWebApi.Model;

namespace TestWebApi.Abstractions;

public interface IUserService
{
    public Token Register(string name, string email, string password);
    public Token Login(string email, string password);
    public User EditUserName(string userId, string name);
}