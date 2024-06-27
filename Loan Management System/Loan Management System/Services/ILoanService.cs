using Loan_Management_System.DTOs;
using Loan_Management_System.Models;

namespace Loan_Management_System.Services
{
    public interface ILoanService
    {
        public Task<List<LoanDTO>> GetAllLoans();
        public Task<List<AppliedByUser>> GetAllApplications();
        public Task<List<AppliedByUser>> GetAllApproved();
        public Task<List<AppliedByUser>> GetAllPending();
        public Task<List<AppliedByUser>> GetAllRejected();
        public Task<List<AppliedByUser>> GetLoansByUser(int userId);
        public Task ApplyNewLoan(AppliedLoan newLoan);
        public Task ApproveLoan(int loanId);
        public Task RejectLoan(int loanId);
        public Task DeleteLoan(int loanId);
    }
}
