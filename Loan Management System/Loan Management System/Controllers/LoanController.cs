using Loan_Management_System.DTOs;
using Loan_Management_System.Models;
using Loan_Management_System.Repository.LoanData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        public readonly LoanRepository _loanRepo;

        public LoanController(LoanRepository loanRepo)
        {
            _loanRepo = loanRepo;
        }
        [HttpGet("getAllLoans")]
        public async Task<IActionResult> getAllLoans()
        {
            List<Loan> loans = await _loanRepo.GetAllLoans();
            return Ok(new {loans});
        }
        [HttpGet("getAllApplications")]
        public async Task<IActionResult> getAllApplications()
        {
            List<AppliedByUser> loans = await _loanRepo.GetAllApplications();
            return Ok(new { loans });
        }
        [HttpGet("getAllApproved")]
        public async Task<IActionResult> getAllApproved()
        {
            List<AppliedByUser> loans = await _loanRepo.GetAllApproved();
            return Ok(new { loans });
        }
        [HttpGet("getAllPending")]
        public async Task<IActionResult> getAllPending()
        {
            List<AppliedByUser> loans = await _loanRepo.GetAllPending();
            return Ok(new { loans });
        }
        [HttpGet("getAllRejected")]
        public async Task<IActionResult> getAllRejected()
        {
            List<AppliedByUser> loans = await _loanRepo.GetAllRejected();
            return Ok(new { loans });
        }
        [HttpGet("getLoansByUser")]
        public async Task<IActionResult> getLoansByUser(int userId)
        {
            Console.WriteLine(userId);
            List<AppliedByUser> loanList = await _loanRepo.GetLoansByUser(userId);
            return Ok(new { loanList });
        }
        [HttpPost("applyNewLoan")]
        public IActionResult applyNewLoan([FromBody] AppliedLoan newLoan)
        {
            newLoan.Status = Status.pending;
            newLoan.DateApplied = DateTime.Now;
            _loanRepo.ApplyNewLoan(newLoan);
            return Ok("Loan Applied Successfully!");
        }

        [HttpPatch("approveLoan")]
        public IActionResult approveLoan(int loanId)
        {
            _loanRepo.ApproveLoan(loanId);
            return Ok("Loan Approved");
        }
        [HttpPatch("rejectLoan")]
        public IActionResult rejectLoan(int loanId)
        {
            _loanRepo.RejectLoan(loanId);
            return Ok("Loan Rejected");
        }
        [HttpDelete("deleteLoan")]
        public IActionResult deleteLoan(int loanId)
        {
            _loanRepo.DeleteLoan(loanId);
            return Ok("Loan Deleted");
        }
    }
}
