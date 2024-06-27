using Loan_Management_System.DTOs;
using Loan_Management_System.Models;

namespace Loan_Management_System.Repository.LoanData
{
    public interface ILoanRepository
    {
        Task<List<Loan>> GetAllLoans();
        Task<List<AppliedByUser>> GetAllApplications();
        Task<List<AppliedByUser>> GetAllApproved();
        Task<List<AppliedByUser>> GetAllPending();
        Task<List<AppliedByUser>> GetAllRejected();
        Task<List<AppliedByUser>> GetLoansByUser(int userId);
        Task ApplyNewLoan(AppliedLoan newLoan);
        Task ApproveLoan(int loanId);
        Task RejectLoan(int loanId);
        Task DeleteLoan(int loanId);
    }
}
