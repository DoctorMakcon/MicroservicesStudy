using Microsoft.EntityFrameworkCore;
using PlatformService.Database.Models;

namespace PlatformService.Database.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Platform> Platforms { get; set; }
    }
}
