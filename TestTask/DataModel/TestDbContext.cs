using Microsoft.EntityFrameworkCore;

namespace TestTask.DataModel
{
    public class TestDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Incident>().HasKey(e => e.IncidentName);
            modelBuilder.Entity<Contact>().HasKey(e => e.Id);
            modelBuilder.Entity<Account>().HasKey(e => e.Name);
            modelBuilder.Entity<Account>().HasIndex(s => s.Name).IsUnique();
            modelBuilder.Entity<Contact>().HasIndex(s => s.Email).IsUnique();
            modelBuilder.Entity<Contact>().Property(f => f.Id).ValueGeneratedOnAdd();
        }

        public TestDbContext(DbContextOptions<TestDbContext> oprions): base(oprions)
        {
        }

        public DbSet<Contact> Contact { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Incident> Incident { get; set; }
    }
}
