using Access_Management_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Access_Management_Application.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AccessLog> AccessLogs { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
    }
}