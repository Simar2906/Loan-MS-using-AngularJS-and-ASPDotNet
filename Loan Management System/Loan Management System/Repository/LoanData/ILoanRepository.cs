using Loan_Management_System.DTOs;

namespace Loan_Management_System.Repository.LoanData
{
    public interface ILoanRepository
    {
        Task<List<Loan>> GetAllLoans();
    }
}
