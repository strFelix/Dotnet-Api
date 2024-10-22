using Microsoft.EntityFrameworkCore;
using api.Models;
namespace api.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }

    public DbSet<User> Users { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<UserResponse> UserResponses { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region User
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
        #endregion

        #region Answer
        modelBuilder.Entity<Answer>()
            .ToTable("answers");

        modelBuilder.Entity<Answer>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<Answer>()
            .HasIndex(a => a.Title);
        #endregion

        #region Option
        modelBuilder.Entity<Option>()
            .ToTable("options");

        modelBuilder.Entity<Option>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Option>()
            .HasIndex(o => o.Description);
        #endregion

        #region UserReponse
        modelBuilder.Entity<UserResponse>()
            .ToTable ("responses");


        modelBuilder.Entity<UserResponse>()
            .HasKey(ur => new { ur.UserId, ur.AnswerId });
        
        modelBuilder.Entity<UserResponse>()
            .HasAlternateKey(ur => new { ur.UserId, ur.AnswerId });

        modelBuilder.Entity<UserResponse>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.Responses)
            .HasForeignKey(ur => ur.UserId)
            .HasPrincipalKey(u => u.Id);

        modelBuilder.Entity<UserResponse>()
            .HasOne(ur => ur.Answer)
            .WithMany(a => a.Responses)
            .HasForeignKey(ur => ur.AnswerId)
            .HasPrincipalKey(a => a.Id);
        #endregion

        #region Option
        modelBuilder.Entity<Option>()
            .ToTable("options");

        modelBuilder.Entity<Option>()
            .HasKey(o => o.Id);

        modelBuilder.Entity<Option>()
            .HasOne(o => o.Answer)
            .WithMany(a => a.Options)
            .HasForeignKey(o  => o.AnswerId)
            .HasPrincipalKey (a => a.Id);
        #endregion
    }
}

