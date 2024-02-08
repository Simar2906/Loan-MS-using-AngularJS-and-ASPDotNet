using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Loan_Management_System.Data;
using Loan_Management_System.DTOs;
using Loan_Management_System.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Loan_Management_System.Repository.LoanData
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ApplicationDbContext _dbContextEF;
        private readonly IDbConnection _db;

        public LoanRepository(ApplicationDbContext dbContextEF, IConfiguration configuration)
        {
            _db = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _dbContextEF = dbContextEF;
        }

        public async Task<List<Loan>> GetAllLoans()
        {
            return await _dbContextEF.Loans.ToListAsync();
        }

        public async Task<List<AppliedByUser>> GetLoansByUser(int userId)
        {
            var sql = @"SELECT al.""Id"", u.""Email"", u.""Gender"", u.""Name"", 
                               u.""Password"", u.""Role"", u.""UserPic"", l.""Logo"", l.""Title"", 
                               l.""LoanAmount"", l.""InterestRates"", al.""AppliedAmount"", al.""AppliedRate"", 
                               l.""MinCreditScore"", al.""TermLength"", l.""ProcessingFee"", u.""Employer"", u.""Salary"", 
                               u.""Designation"", al.""Status"", al.""DateApplied"", al.""LoanId"" 
                        FROM public.""AppliedLoans"" al 
                        LEFT JOIN public.""Users"" u ON u.""Id"" = al.""UserId"" 
                        LEFT JOIN public.""Loans"" l ON l.""Id"" = al.""LoanId"" 
                        WHERE al.""UserId"" = @UserId";

            return (await _db.QueryAsync<AppliedByUser>(sql, new { UserId = userId })).AsList();
        }
    }
}
