using TestWebApi.Abstractions;
using TestWebApi.Context;
using TestWebApi.Model;

namespace TestWebApi.Repositories;

public class UserRepository: IUserRepository
{
    private readonly ApplicationContext _context;
    
    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public User? Register(User user)
    { 
        _context.Users.Add(user);
        var result = _context.SaveChanges();

        return result > 0 ? user : null;
    }
}