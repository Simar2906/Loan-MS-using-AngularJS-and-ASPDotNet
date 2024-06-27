using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Loan_Management_System.Data;
using Loan_Management_System.DTOs;
using Loan_Management_System.Models;
using Loan_Management_System.Repository.UserData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] SignupDTO userData)
        {
            try
            {
                User user = await _userRepo.CreateUser(userData);
                var token = GenerateToken(user);
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
                User user = await _userRepo.GetUser(credentials);
                if(user == null)
                {
                    return Unauthorized();
                }
                else
                {
                    var token = GenerateToken(user);
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
            await _userRepo.DeleteUser(userId);
            return Ok("user deleted");
        }

        private string GenerateToken(User userDetails)
        {
            var claims = new List<Claim>
            {
                new Claim("id", userDetails.Id.ToString()),
                new Claim("email", userDetails.Email),
                new Claim("firstName", userDetails.FirstName),
                new Claim("lastName", userDetails.LastName ?? String.Empty),
                new Claim("gender", userDetails.Gender.ToString()),
                new Claim("password", userDetails.Password),
                new Claim("role", userDetails.Role.ToString()),
                new Claim("salary", userDetails.Salary.ToString()),
                new Claim("employer", userDetails.Employer),
                new Claim("designation", userDetails.Designation),
                new Claim("userPicPath", userDetails.ProfilePicture.FilePath)
            };
            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefgh12345678abcdefgh12345678")), SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(7));

            var token = new JwtSecurityToken(
                "SimarAuthApi",
                "SiamrLoanApp",
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
