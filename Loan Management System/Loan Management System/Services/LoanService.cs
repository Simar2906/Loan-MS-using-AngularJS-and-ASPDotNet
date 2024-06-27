using Loan_Management_System.DTOs;
using Loan_Management_System.Models;
using Loan_Management_System.Repository.LoanData;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Services
{
    public class LoanService : ILoanService
    {
        private readonly LoanRepository _loanRepo;
        public LoanService(LoanRepository loanRepo)
        {
            _loanRepo = loanRepo;
        }
        public async Task<List<LoanDTO>> GetAllLoans()
        {
            List<Loan> allLoans = await _loanRepo.GetAllLoans();

            List<LoanDTO> loans = allLoans.Select(l => new LoanDTO
            {
                Id = l.Id,
                Title = l.Title,
                MinCreditScore = l.MinCreditScore,
                TermLength = l.TermLength,
                ProcessingFee = l.ProcessingFee,
                LogoFilePath = l.LogoPicture.FilePath,
                MinLoanAmount = l.MinLoanAmount,
                MaxLoanAmount = l.MaxLoanAmount,
                MinInterestRate = l.MinInterestRate,
                MaxInterestRate = l.MaxInterestRate
            }).ToList();
            return loans;
        }

        public async Task<List<AppliedByUser>> GetAllApplications()
        {
            List<AppliedByUser> loans = await _loanRepo.GetAllApplications();
            return loans;
        }

        public async Task<List<AppliedByUser>> GetAllApproved()
        {
            List<AppliedByUser> loans = await _loanRepo.GetAllApproved();
            return loans;
        }
        
        public async Task<List<AppliedByUser>> GetAllPending()
        {
            List<AppliedByUser> loans = await _loanRepo.GetAllPending();
            return loans;
        }

        public async Task<List<AppliedByUser>> GetAllRejected()
        {
            List<AppliedByUser> loans = await _loanRepo.GetAllRejected();
            return loans;
        }

        public async Task<List<AppliedByUser>> GetLoansByUser(int userId)
        {
            List<AppliedByUser> loans = await _loanRepo.GetLoansByUser(userId);
            return loans;
        }

        public async Task ApplyNewLoan(AppliedLoan newLoan)
        {
            newLoan.Status = Status.pending;
            newLoan.DateApplied = DateTime.Now;
            await _loanRepo.ApplyNewLoan(newLoan);
        }
        public async Task ApproveLoan(int loanId)
        {
            await _loanRepo.ApproveLoan(loanId);
        }
        public async Task RejectLoan(int loanId)
        {
            await _loanRepo.RejectLoan(loanId);
        }
        public async Task DeleteLoan(int loanId)
        {
            await _loanRepo.DeleteLoan(loanId);
        }
    }
}
