using TestWebApi.Abstractions;
using TestWebApi.Model;

namespace TestWebApi.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public User? Register(string name, string email, string password)
    {
        var user = new User
        {
            Email = email,
            FullName = name,
            Password = password
        };

        return _userRepository.Register(user);
    }
}