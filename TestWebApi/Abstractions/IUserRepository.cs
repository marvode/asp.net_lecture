using TestWebApi.DataTransferObjects;
using TestWebApi.Model;

namespace TestWebApi.Abstractions;

public interface IUserRepository
{
    public User? Register(User user);
    
    public User? FindByEmail(string email);

    public User? Login(string email, string password);

    public User EditUserName(string userId, string name);

    public PaginatorResponseDTO<IEnumerable<User>> GetAll(int pageSize, int pageNumber);
}