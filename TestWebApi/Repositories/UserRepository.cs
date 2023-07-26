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

    public User? FindByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }

    public User? Login(string email, string password)
    {
        return _context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
    }

    public User EditUserName(string userId, string name)
    {
        var user = _context.Users.Find(userId);
        user.FullName = name;
        var result = _context.SaveChanges();
        
        if (result < 1)
            throw new Exception("could not save to db");
        
        return user;
    }
}