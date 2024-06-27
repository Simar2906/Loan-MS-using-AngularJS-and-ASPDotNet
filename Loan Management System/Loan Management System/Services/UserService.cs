using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Loan_Management_System.DTOs;
using Loan_Management_System.Models;
using Loan_Management_System.Repository.UserData;
using Microsoft.IdentityModel.Tokens;

namespace Loan_Management_System.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepo;
        public UserService(UserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<string> CreateUser(SignupDTO userData)
        {
            string base64String = userData.UserPic;
            string base64Data = base64String.Substring(base64String.IndexOf(',') + 1);
            string uniqueFileName = await SaveFileAsync(Convert.FromBase64String(base64Data), userData.FileName);
            DTOs.File profilePicture = new DTOs.File
            {
                FilePath = uniqueFileName
            };
            await _userRepo.SaveFile(profilePicture);
            int profilePictureId = profilePicture.Id;
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userData.Password, salt);
            
            var user = await _userRepo.AddUser(userData, hashedPassword, salt, profilePictureId);
            var token = GenerateToken(user);
            return token;
        }

        private async Task<string> SaveFileAsync(byte[] fileBytes, string fileName)
        {
            string projectRoot = Directory.GetCurrentDirectory();
            string uploadsFolder = Path.Combine(projectRoot, "src", "assets");

            // Ensure uploadsFolder exists; create if it doesn't
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = null;

            if (fileBytes != null && fileBytes.Length > 0)
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Write file to disk
                await System.IO.File.WriteAllBytesAsync(filePath, fileBytes);

                uniqueFileName = Path.Combine("src", "assets", uniqueFileName);


            }
            return uniqueFileName;
        }

        public async Task<string> GetUser(LoginFormData credentials)
        {
            var user = await _userRepo.GetUser(credentials);
            if (user != null && BCrypt.Net.BCrypt.Verify(credentials.password, user.Password))
            {
                var token = GenerateToken(user);
                return token;
            }
            return null;
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

        public async Task DeleteUser(int userId)
        {
            await _userRepo.DeleteUser(userId);
        }
    }
}
