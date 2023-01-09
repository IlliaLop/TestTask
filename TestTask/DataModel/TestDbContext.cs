using Microsoft.EntityFrameworkCore;

namespace TestTask.DataModel
{
    public class TestDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Contacts>().HasKey(e => e.Email);
        }

        public TestDbContext(DbContextOptions<TestDbContext> oprions): base(oprions)
        {
        }

        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Incidents> Incidents { get; set; }
    }
}
