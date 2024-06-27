using Loan_Management_System.DTOs;
using Loan_Management_System.Models;
using Loan_Management_System.Repository.LoanData;
using Loan_Management_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        public readonly LoanService _loanSvc;

        public LoanController(LoanService loanSvc)
        {
            _loanSvc = loanSvc;
        }
        [HttpGet("getAllLoans")]
        public async Task<IActionResult> getAllLoans()
        {
            try
            {
                List<LoanDTO> loans = await _loanSvc.GetAllLoans();
                return Ok(new { loans });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpGet("getAllApplications")]
        public async Task<IActionResult> getAllApplications()
        {
            try
            {
                List<AppliedByUser> loans = await _loanSvc.GetAllApplications();
                return Ok(new { loans });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpGet("getAllApproved")]
        public async Task<IActionResult> getAllApproved()
        {
            try
            {
                List<AppliedByUser> loans = await _loanSvc.GetAllApproved();
                return Ok(new { loans });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpGet("getAllPending")]
        public async Task<IActionResult> getAllPending()
        {
            try
            {
                List<AppliedByUser> loans = await _loanSvc.GetAllPending();
                return Ok(new { loans });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpGet("getAllRejected")]
        public async Task<IActionResult> getAllRejected()
        {
            try
            {
                List<AppliedByUser> loans = await _loanSvc.GetAllRejected();
                return Ok(new { loans });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpGet("getLoansByUser")]
        public async Task<IActionResult> getLoansByUser(int userId)
        {
            try
            {
                List<AppliedByUser> loanList = await _loanSvc.GetLoansByUser(userId);
                return Ok(new { loanList });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpPost("applyNewLoan")]
        public async Task<IActionResult> applyNewLoan([FromBody] AppliedLoan newLoan)
        {
            try
            {
                await _loanSvc.ApplyNewLoan(newLoan);
                return Ok("Loan Applied Successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPatch("approveLoan")]
        public async Task<IActionResult> approveLoan(int loanId)
        {
            try
            {
                await _loanSvc.ApproveLoan(loanId);
                return Ok("Loan Approved");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpPatch("rejectLoan")]
        public async Task<IActionResult> rejectLoan(int loanId)
        {
            try
            {
                await _loanSvc.RejectLoan(loanId);
                return Ok("Loan Rejected");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpDelete("deleteLoan")]
        public async Task<IActionResult> deleteLoan(int loanId)
        {
            try
            {
                await _loanSvc.DeleteLoan(loanId);
                return Ok("Loan Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
