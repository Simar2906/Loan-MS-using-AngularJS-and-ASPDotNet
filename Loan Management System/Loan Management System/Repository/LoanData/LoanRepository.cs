using System.Data;
using Dapper;
using Loan_Management_System.Data;
using Loan_Management_System.DTOs;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
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
            return await _dbContextEF.Loans.Include(l => l.LogoPicture).ToListAsync();
        }
        public async Task<List<AppliedByUser>> GetAllApplications()
        {
            return await _dbContextEF.AppliedLoans
                .Include(al => al.User)
                .Include(al => al.Loan)
                .Select(al => new AppliedByUser
                {
                    Id = al.Id,
                    Email = al.User.Email,
                    Gender = al.User.Gender,
                    FirstName = al.User.FirstName,
                    LastName = al.User.LastName,
                    Password = al.User.Password,
                    Role = al.User.Role,
                    UserPicturePath = al.User.ProfilePicture.FilePath,
                    LoanLogoPath = al.Loan.LogoPicture.FilePath,
                    Title = al.Loan.Title,
                    MinLoanAmount = al.Loan.MinLoanAmount,
                    MaxLoanAmount = al.Loan.MaxLoanAmount,
                    MinInterestRate = al.Loan.MinInterestRate,
                    MaxInterestRate = al.Loan.MaxInterestRate,
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
        public async Task<List<AppliedByUser>> GetAllApproved()
        {
            var sql = @"SELECT al.""Id"", u.""Email"", u.""Gender"", u.""FirstName"", u.""LastName"",
                               u.""Password"", u.""Role"", u.""ProfilePictureFileId"", l.""LogoFileId"", l.""Title"", 
                               l.""MinLoanAmount"", l.""MaxLoanAmount"", l.""MinInterestRate"", l.""MaxInterestRate"", al.""AppliedAmount"", al.""AppliedRate"", 
                               l.""MinCreditScore"", al.""TermLength"", l.""ProcessingFee"", u.""Employer"", u.""Salary"", 
                               u.""Designation"", al.""Status"", al.""DateApplied"", al.""LoanId"", uf.""FilePath"" AS ""UserPicturePath"", lf.""FilePath"" AS ""LoanLogoPath""
                        FROM public.""AppliedLoans"" al 
                        LEFT JOIN public.""Users"" u ON u.""Id"" = al.""UserId""
                        LEFT JOIN public.""Loans"" l ON l.""Id"" = al.""LoanId""
                        INNER JOIN public.""Files"" uf ON u.""ProfilePictureFileId"" = uf.""Id""
                        INNER JOIN public.""Files"" lf ON l.""LogoFileId"" = lf.""Id"" 
                        WHERE al.""Status"" = 'approved'";
            return (await _db.QueryAsync<AppliedByUser>(sql)).AsList();
        }
        public async Task<List<AppliedByUser>> GetAllPending()
        {
            var sql = @"SELECT al.""Id"", u.""Email"", u.""Gender"", u.""FirstName"", u.""LastName"",
                               u.""Password"", u.""Role"", u.""ProfilePictureFileId"", l.""LogoFileId"", l.""Title"", 
                               l.""MinLoanAmount"", l.""MaxLoanAmount"", l.""MinInterestRate"", l.""MaxInterestRate"", al.""AppliedAmount"", al.""AppliedRate"", 
                               l.""MinCreditScore"", al.""TermLength"", l.""ProcessingFee"", u.""Employer"", u.""Salary"", 
                               u.""Designation"", al.""Status"", al.""DateApplied"", al.""LoanId"", uf.""FilePath"" AS ""UserPicturePath"", lf.""FilePath"" AS ""LoanLogoPath""
                        FROM public.""AppliedLoans"" al 
                        LEFT JOIN public.""Users"" u ON u.""Id"" = al.""UserId""
                        LEFT JOIN public.""Loans"" l ON l.""Id"" = al.""LoanId""
                        INNER JOIN public.""Files"" uf ON u.""ProfilePictureFileId"" = uf.""Id""
                        INNER JOIN public.""Files"" lf ON l.""LogoFileId"" = lf.""Id""
                        WHERE al.""Status"" = 'pending'";
            return (await _db.QueryAsync<AppliedByUser>(sql)).AsList();
        }
        public async Task<List<AppliedByUser>> GetAllRejected()
        {
            var sql = @"SELECT al.""Id"", u.""Email"", u.""Gender"", u.""FirstName"", u.""LastName"",
                               u.""Password"", u.""Role"", u.""ProfilePictureFileId"", l.""LogoFileId"", l.""Title"", 
                               l.""MinLoanAmount"", l.""MaxLoanAmount"", l.""MinInterestRate"", l.""MaxInterestRate"", al.""AppliedAmount"", al.""AppliedRate"", 
                               l.""MinCreditScore"", al.""TermLength"", l.""ProcessingFee"", u.""Employer"", u.""Salary"", 
                               u.""Designation"", al.""Status"", al.""DateApplied"", al.""LoanId"", uf.""FilePath"" AS ""UserPicturePath"", lf.""FilePath"" AS ""LoanLogoPath""
                        FROM public.""AppliedLoans"" al 
                        LEFT JOIN public.""Users"" u ON u.""Id"" = al.""UserId""
                        LEFT JOIN public.""Loans"" l ON l.""Id"" = al.""LoanId""
                        INNER JOIN public.""Files"" uf ON u.""ProfilePictureFileId"" = uf.""Id""
                        INNER JOIN public.""Files"" lf ON l.""LogoFileId"" = lf.""Id""
                        WHERE al.""Status"" = 'rejected'";
            return (await _db.QueryAsync<AppliedByUser>(sql)).AsList();
        }

        public async Task<List<AppliedByUser>> GetLoansByUser(int userId)
        {
            var sql = @"SELECT al.""Id"", u.""Email"", u.""Gender"", u.""FirstName"", u.""LastName"",
                               u.""Password"", u.""Role"", u.""ProfilePictureFileId"", l.""LogoFileId"", l.""Title"", 
                               l.""MinLoanAmount"", l.""MaxLoanAmount"", l.""MinInterestRate"", l.""MaxInterestRate"", al.""AppliedAmount"", al.""AppliedRate"", 
                               l.""MinCreditScore"", al.""TermLength"", l.""ProcessingFee"", u.""Employer"", u.""Salary"", 
                               u.""Designation"", al.""Status"", al.""DateApplied"", al.""LoanId"", uf.""FilePath"" AS ""UserPicturePath"", lf.""FilePath"" AS ""LoanLogoPath""
                        FROM public.""AppliedLoans"" al 
                        LEFT JOIN public.""Users"" u ON u.""Id"" = al.""UserId""
                        LEFT JOIN public.""Loans"" l ON l.""Id"" = al.""LoanId""
                        INNER JOIN public.""Files"" uf ON u.""ProfilePictureFileId"" = uf.""Id""
                        INNER JOIN public.""Files"" lf ON l.""LogoFileId"" = lf.""Id""
                        WHERE al.""UserId"" = @UserId";

            return (await _db.QueryAsync<AppliedByUser>(sql, new { UserId = userId })).AsList();
        }

        public async Task ApplyNewLoan(AppliedLoan newLoan)
        {
            var sql = @"INSERT INTO public.""AppliedLoans""(""UserId"", ""AppliedAmount"",
								  ""AppliedRate"", ""TermLength"", ""Status"", ""DateApplied"", ""LoanId"")
			VALUES(@UserId, @AppliedAmount, @AppliedRate, @TermLength, @Status::""Status"", @DateApplied, @LoanId);";
            _db.Execute(sql, new
            {
                UserId = newLoan.UserId,
                AppliedAmount = newLoan.AppliedAmount,
                AppliedRate = newLoan.AppliedRate,
                TermLength = newLoan.TermLength,
                Status = newLoan.Status.ToString(),
                DateApplied = newLoan.DateApplied,
                LoanId = newLoan.LoanId
            });
            return;
        }

        public async Task ApproveLoan(int loanId)
        {
            var sql = @"UPDATE public.""AppliedLoans"" SET ""Status"" = 'approved' WHERE ""Id"" = @loanId";
            _db.Execute(sql, new
            {
                loanId = loanId
            });
        }
        public async Task RejectLoan(int loanId)
        {
            var sql = @"UPDATE public.""AppliedLoans"" SET ""Status"" = 'rejected' WHERE ""Id"" = @loanId";
            _db.Execute(sql, new
            {
                loanId = loanId
            });
        }
        public async Task DeleteLoan(int loanId)
        {
            var sql = @"DELETE FROM public.""AppliedLoans"" WHERE ""Id"" = @loanId";

            _db.Execute(sql, new { loanId });
        }
    }
}
