using Loan_Management_System.DTOs;
using Microsoft.EntityFrameworkCore;
using static Dapper.SqlMapper;

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
        public DbSet<DTOs.File> Files { get; set; }
    }
}
