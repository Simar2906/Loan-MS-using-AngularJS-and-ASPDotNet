using Loan_Management_System.Data;
using Loan_Management_System.Models;
using Loan_Management_System.Repository.UserData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidationController : ControllerBase
    {
        private readonly UserRepository _userRepo;
        public ValidationController(UserRepository userRepo)
        {
            _userRepo = userRepo;
        }   
        
        public async Task<IActionResult> ValidateUser([FromBody] LoginFormData credentials)
        {
            try
            {
                var user = await _userRepo.GetUser(credentials);
                if(user == null)
                {
                    return Unauthorized("Incorrect Credentials!");
                }
                else
                {
                    return Ok("Logged in Successfully!");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

    }
}
