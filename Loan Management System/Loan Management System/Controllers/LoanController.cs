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
        [HttpPost("getLoansByUser")]
        public async Task<IActionResult> getLoansByUser(int UserId)
        {
            List<AppliedByUser> loanList = await _loanRepo.GetLoansByUser(UserId);

            return Ok(new { loanList });
        }
    }
}
