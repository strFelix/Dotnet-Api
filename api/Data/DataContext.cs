using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.Metrics;
namespace api.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }

    public DbSet<User> Users { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<UserResponse> UserResponses { get; set; }

    // preload answer
    public class AnswerSeedDataConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasData(
                new Answer() { Id = 1, Title = "What is a correct syntax to output 'Hello World' in C#?", CorrectOption = 3 },
                new Answer() { Id = 2, Title = "Which data type is used to create a variable that should store text?", CorrectOption = 4 }
            );
        }
    }

    // preload option
    public class OptionSeedDataConfiguration : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.HasData(
                new Option() { AnswerId = 1, OptionNumber = 1, Description = "print('Hello World')" },
                new Option() { AnswerId = 1, OptionNumber = 2, Description = "cout < < 'Hello World'" },
                new Option() { AnswerId = 1, OptionNumber = 3, Description = "System.out.printLn('Hello World')" },
                new Option() { AnswerId = 1, OptionNumber = 4, Description = "Console.WriteLine('Hello World')" },

                new Option() { AnswerId = 2, OptionNumber = 1, Description = "myString" },
                new Option() { AnswerId = 2, OptionNumber = 2, Description = "Txt" },
                new Option() { AnswerId = 2, OptionNumber = 3, Description = "str" },
                new Option() { AnswerId = 2, OptionNumber = 4, Description = "string" }
            );
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // insert pre load data
        modelBuilder.ApplyConfiguration(new AnswerSeedDataConfiguration());
        modelBuilder.ApplyConfiguration(new OptionSeedDataConfiguration());

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

        #region UserReponse
        modelBuilder.Entity<UserResponse>()
            .ToTable("responses");

        modelBuilder.Entity<UserResponse>()
            .HasKey(ur => new { ur.UserId, ur.AnswerId });

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
           .HasKey(o => new { o.AnswerId, o.OptionNumber });

        modelBuilder.Entity<Option>()
            .HasOne(o => o.Answer)
            .WithMany(a => a.Options)
            .HasForeignKey(o => o.AnswerId)
            .HasPrincipalKey(a => a.Id);
        #endregion
    }
}