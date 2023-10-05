using Microsoft.EntityFrameworkCore;
using QiTask.Models;

namespace QiTask.Data.Data
{
    public class DataContext : DbContext
    {
        
        public DataContext()
        {
        }
        
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the one-to-many relationship
            modelBuilder.Entity<Note>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notes)
                .HasForeignKey(n => n.UserId);
        }
    }
}