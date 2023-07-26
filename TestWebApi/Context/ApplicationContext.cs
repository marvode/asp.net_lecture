using Microsoft.EntityFrameworkCore;
using TestWebApi.Model;

namespace TestWebApi.Context;

public class ApplicationContext: DbContext
{
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}