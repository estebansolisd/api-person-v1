using Microsoft.EntityFrameworkCore;

using person_api_1.Models;

namespace person_api_1.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonHistory> PersonHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<PersonHistory>()
                .HasKey(ph => ph.HistoryId);
        }
    }
}