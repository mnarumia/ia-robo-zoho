
using Microsoft.EntityFrameworkCore;

namespace RoboIAZoho.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ZohoConfig> ZohoConfigs { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
