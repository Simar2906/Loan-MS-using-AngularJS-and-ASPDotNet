using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Loan_Management_System.Data;
using Loan_Management_System.DTOs;
using Loan_Management_System.Repository.UserData;
using Loan_Management_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidationController : ControllerBase
    {
        private readonly UserService _userSvc;
        public ValidationController(UserService userSvc)
        {
            _userSvc = userSvc;
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] SignupDTO userData)
        {
            try
            {
                var token = await _userSvc.CreateUser(userData);
                
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpPost("ValidateUser")]
        public async Task<IActionResult> ValidateUser([FromBody] LoginFormData credentials)
        {
            try
            {
                var token = await _userSvc.GetUser(credentials);
                if(token == null)
                {
                    return Unauthorized();
                }
                else
                {
                    return Ok(new { Token = token });
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                await _userSvc.DeleteUser(userId);
                return Ok("user deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
