using Loan_Management_System.DTOs;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using static Dapper.SqlMapper;

namespace Loan_Management_System.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<AppliedLoan> AppliedLoans { get; set; }
        public DbSet<DTOs.File> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Register the enum type
            modelBuilder.HasPostgresEnum<Gender>("public", "Gender");
            modelBuilder.HasPostgresEnum<Role>("public", "Role");
            modelBuilder.HasPostgresEnum<Status>("public", "Status");

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Gender)
                    .HasConversion<string>();

                entity.Property(e => e.Role)
                    .HasConversion<string>();
            });

            //modelBuilder.Entity<AppliedLoan>(entity =>
            //{
            //    entity.Property(l=> l.Status)
            //        .HasConversion<string>();
            //});
            modelBuilder.Entity<AppliedLoan>()
                .Property(al => al.Status)
                .HasConversion(
                    v => v.ToString(), // Convert to string for storing in the database
                    v => (Status)Enum.Parse(typeof(Status), v) // Convert to enum when reading from the database
                )
                .HasColumnType("Status"); // Explicitly specify the column type
        }
    }
}
