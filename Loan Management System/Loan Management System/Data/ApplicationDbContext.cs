using Loan_Management_System.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Loan_Management_System.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<AppliedLoan> AppliedLoans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Salary)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<User>()
                .Property(u => u.Email).HasColumnType("text");
                base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppliedLoan>()
                .HasOne(al => al.Loan)
                .WithMany(l => l.AppliedLoans)
                .HasForeignKey(al => al.LoanId);

            modelBuilder.Entity<AppliedLoan>()
                .HasOne(al => al.User)
                .WithMany(u =>u.AppliedLoans)
                .HasForeignKey(al => al.UserId);
        }
    }
}
