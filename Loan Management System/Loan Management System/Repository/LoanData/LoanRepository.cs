using Loan_Management_System.Data;
using Loan_Management_System.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Loan_Management_System.Repository.LoanData
{
    public class LoanRepository:ILoanRepository
    {
        private readonly ApplicationDbContext _dbContextEF;
        public LoanRepository(ApplicationDbContext dbContextEF)
        {
            _dbContextEF = dbContextEF;
        }

        public async Task<List<Loan>> GetAllLoans()
        {
            return await _dbContextEF.Loans.ToListAsync();
        }
    }
}
