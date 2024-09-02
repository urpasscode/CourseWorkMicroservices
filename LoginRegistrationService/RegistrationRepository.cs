using LoginRegistrationService.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginRegistrationService
{
    public class RegistrationRepository:DbContext
    {
        public RegistrationRepository(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Registration> Users { get; set; }
    }
}
