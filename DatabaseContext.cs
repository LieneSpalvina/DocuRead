using DocumentReaderApp.Models;
using Microsoft.EntityFrameworkCore;


namespace DocumentReaderApp.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        private readonly string _connectionString;

        public DatabaseContext(string dbFile = "document_reader.db")
        {
            _connectionString = $"Data Source={dbFile}";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
