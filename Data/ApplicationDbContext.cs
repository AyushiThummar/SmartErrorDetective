using Microsoft.EntityFrameworkCore;
using SmartError.API.Models;

namespace SmartError.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ErrorLog> ErrorLogs { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Severity> Severities { get; set; }
    }
}