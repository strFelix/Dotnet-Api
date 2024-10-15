using Microsoft.EntityFrameworkCore;
using api.Models;
namespace api.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
                .ToTable("users");

        modelBuilder.Entity<User>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<User>()
            .HasIndex(c => c.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<User>()
            .Property(c => c.Name)
            .HasMaxLength(60);

        modelBuilder.Entity<User>()
            .Property(c => c.Email)
            .HasMaxLength(255);

        modelBuilder.Entity<User>()
            .Property(c => c.PhoneNumber)
            .HasMaxLength(20);
    }  
}

