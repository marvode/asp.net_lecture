using System.Security.Authentication;
using TestWebApi.Abstractions;
using TestWebApi.Constants;
using TestWebApi.DataTransferObjects;
using TestWebApi.Helpers;
using TestWebApi.Model;

namespace TestWebApi.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }
    
    public Token Register(string name, string email, string password)
    {
        var userExist = _userRepository.FindByEmail(email);
        if (userExist != null)
            throw new Exception("email already exist");

        var user = new User
        {
            Email = email,
            FullName = name,
            Password = PasswordHasher.HashPassword(password, out var salt),
            Salt = salt,
            Role = UserRole.User
        };

        var result = _userRepository.Register(user);

        if (result == null)
            throw new Exception("something went wrong");

        return new Token
        {
            AccessToken = new Jwt(_configuration).Generate(user.Id, user.Email)
        };
    }
    
    public Token Login(string email, string password)
    {
        var user = _userRepository.FindByEmail(email);
        if (user is null)
            throw new AuthenticationException("User is not authenticated");

        var isValid = PasswordHasher.VerifyPassword(password, user.Password, user.Salt);
        
        if (!isValid)
            throw new AuthenticationException("User is not authenticated");

        return new Token
        {
            AccessToken = new Jwt(_configuration).Generate(user.Id, user.Email)
        };
    }

    public User EditUserName(string userId, string name)
    {
        return _userRepository.EditUserName(userId, name);
    }

    public PaginatorResponseDTO<IEnumerable<User>> GetAll(int pageSize, int pageNumber)
    {
        return _userRepository.GetAll(pageSize, pageNumber);
    }
}