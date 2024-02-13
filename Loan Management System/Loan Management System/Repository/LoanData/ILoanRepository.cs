using Loan_Management_System.DTOs;
using Loan_Management_System.Models;

namespace Loan_Management_System.Repository.LoanData
{
    public interface ILoanRepository
    {
        Task<List<Loan>> GetAllLoans();
        Task<List<AppliedByUser>> GetLoansByUser(int userId);
        void ApplyNewLoan(AppliedLoan newLoan);
    }
}
