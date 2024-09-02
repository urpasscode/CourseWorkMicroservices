using AccountService.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountService
{
    public class UserRepository : DbContext
    {
        public UserRepository(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Users { get; set; }
    }
}