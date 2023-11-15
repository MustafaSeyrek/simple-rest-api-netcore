using Microsoft.EntityFrameworkCore;
using WebApi.Models.Entities;

namespace WebApi.Database
{
    public class AppConnectionDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                 .UseMySql("server=localhost;port=3306;user=XXX;password=XXX;database=web-api", new MySqlServerVersion(new Version(8, 0, 27)), null)
                 .UseLoggerFactory(LoggerFactory.Create(b => b
                 .AddConsole()
                 .AddFilter(level => level >= LogLevel.Information)))
                 .EnableSensitiveDataLogging()
                 .EnableDetailedErrors();
        }
    }
}
