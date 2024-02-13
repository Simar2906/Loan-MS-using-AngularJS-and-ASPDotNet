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
        public async Task<List<AppliedByUser>> GetAllApplications()
        {
            var sql = @"SELECT al.""Id"", u.""Email"", u.""Gender"", u.""Name"", 
                               u.""Password"", u.""Role"", u.""UserPic"", l.""Logo"", l.""Title"", 
                               l.""LoanAmount"", l.""InterestRates"", al.""AppliedAmount"", al.""AppliedRate"", 
                               l.""MinCreditScore"", al.""TermLength"", l.""ProcessingFee"", u.""Employer"", u.""Salary"", 
                               u.""Designation"", al.""Status"", al.""DateApplied"", al.""LoanId"" 
                        FROM public.""AppliedLoans"" al 
                        LEFT JOIN public.""Users"" u ON u.""Id"" = al.""UserId"" 
                        LEFT JOIN public.""Loans"" l ON l.""Id"" = al.""LoanId""";
            return (await _db.QueryAsync<AppliedByUser>(sql)).AsList();
        }
        public async Task<List<AppliedByUser>> GetLoansByUser(int userId)
        {
            return await _dbContextEF.AppliedLoans
                .Where(al => al.UserId == userId)
                .Select(al => new AppliedByUser
                {
                    Id = al.Id,
                    Email = al.User.Email,
                    Gender = al.User.Gender,
                    Name = al.User.Name,
                    Password = al.User.Password,
                    Role = al.User.Role,
                    UserPic = al.User.UserPic,
                    Logo = al.Loan.Logo,
                    Title = al.Loan.Title,
                    LoanAmount = al.Loan.LoanAmount,
                    InterestRates = al.Loan.InterestRates,
                    AppliedAmount = al.AppliedAmount,
                    AppliedRate = al.AppliedRate,
                    MinCreditScore = al.Loan.MinCreditScore,
                    TermLength = al.TermLength,
                    ProcessingFee = al.Loan.ProcessingFee,
                    Employer = al.User.Employer,
                    Salary = al.User.Salary,
                    Designation = al.User.Designation,
                    Status = al.Status,
                    DateApplied = al.DateApplied,
                    LoanId = al.LoanId
                })
    .ToListAsync();
        }

        public void ApplyNewLoan(AppliedLoan newLoan)
        {
            var sql = @"INSERT INTO public.""AppliedLoans""(""UserId"", ""AppliedAmount"",
								  ""AppliedRate"", ""TermLength"", ""Status"", ""DateApplied"", ""LoanId"")
			VALUES(@UserId, @AppliedAmount, @AppliedRate, @TermLength, @Status, @DateApplied, @LoanId);";

            _db.Execute(sql, new
            {
                UserId = newLoan.UserId,
                AppliedAmount = newLoan.AppliedAmount,
                AppliedRate = newLoan.AppliedRate,
                TermLength = newLoan.TermLength,
                Status = newLoan.Status,
                DateApplied = newLoan.DateApplied,
                LoanId = newLoan.LoanId
            });
            return;
        }
    }
}
