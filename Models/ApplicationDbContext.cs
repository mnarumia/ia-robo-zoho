using Microsoft.EntityFrameworkCore;

namespace RoboIAZoho.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ZohoConfig> ZohoConfigs { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Project> Projects { get; set; }

        // Adicione estas linhas
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }
        public DbSet<TaskAttachment> TaskAttachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar a chave primária para não ser gerada pelo banco de dados,
            // pois usaremos o ID do Zoho.
            modelBuilder.Entity<TaskItem>().Property(t => t.Id).ValueGeneratedNever();
            modelBuilder.Entity<SubTask>().Property(s => s.Id).ValueGeneratedNever();
            modelBuilder.Entity<TaskAttachment>().Property(a => a.Id).ValueGeneratedNever();
        }
    }
}