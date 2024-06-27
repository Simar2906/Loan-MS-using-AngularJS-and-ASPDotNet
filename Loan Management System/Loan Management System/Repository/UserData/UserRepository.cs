using Loan_Management_System.Data;
using Loan_Management_System.DTOs;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Dapper;
using BCrypt.Net;
using System.Data.Common;
using System.Data;
using Npgsql;

namespace Loan_Management_System.Repository.UserData
{
    public class UserRepository:IUserRepository
    {
        private readonly ApplicationDbContext _dbContextEF;
        private readonly IDbConnection _db;
        public UserRepository(ApplicationDbContext dbContextEF, IConfiguration configuration) 
        {
            _db = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _dbContextEF = dbContextEF;
        }
        public async Task<User> GetUser(LoginFormData credentials)
        {
            var user = await _dbContextEF.Users.Include(u => u.ProfilePicture).FirstOrDefaultAsync(u => u.Email == credentials.email);
            if (user == null)
            {
                return null;
            }
            //var encryptedPWD = BCrypt.Net.BCrypt.HashPassword(credentials.password, user.Salt);
            if (BCrypt.Net.BCrypt.Verify(credentials.password, user.Password))
            {
                return user;
            }
            return null;
        }
        public async Task<User> CreateUser(SignupDTO userData)
        {
            string base64String = userData.UserPic;

            // Remove the prefix if it exists
            string base64Data = base64String.Substring(base64String.IndexOf(',') + 1);

            string uniqueFileName = await SaveFileAsync(Convert.FromBase64String(base64Data), userData.FileName);

            DTOs.File profilePicture = new DTOs.File
            {
                FilePath = uniqueFileName
            };
            _dbContextEF.Files.Add(profilePicture);
            await _dbContextEF.SaveChangesAsync();

            int profilePictureId = profilePicture.Id;
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            // Hash the existing password with the generated salt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userData.Password, salt);
            var parameters = new
            {
                Email = userData.Email,
                Designation = userData.Designation,
                Employer = userData.Employer,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Password = hashedPassword,
                Gender = userData.Gender,
                Role = userData.Role,
                Salary = userData.Salary,
                Salt = salt,
                ProfilePictureFileId = profilePictureId // assuming profilePictureId is already generated
            };
            try
            {

            string sql = @"
        INSERT INTO ""Users"" (""Designation"", ""Email"", ""Employer"", ""FirstName"", ""LastName"", ""Password"", ""Gender"", ""Role"", ""Salary"", ""Salt"", ""ProfilePictureFileId"")
        VALUES (@Designation, @Email, @Employer, @FirstName, @LastName, @Password, @Gender::""Gender"", @Role::""Role"", @Salary, @Salt, @ProfilePictureFileId)"; // If using SQL Server to get the last inserted id
            _db.Execute(sql, parameters);
                var user = await _dbContextEF.Users.FirstOrDefaultAsync(u => u.Email == userData.Email && u.Password == hashedPassword);
                return user;
            }
            catch(Exception ex) { }
            return null;
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
        public async Task DeleteUser(int userId)
        {
            try
            {
                var user = await _dbContextEF.Users
                .Include(u => u.AppliedLoans)
                .Include(u => u.ProfilePicture)// Include loans to delete them too
                .FirstOrDefaultAsync(u => u.Id == userId);

                if (user != null)
                {
                    _dbContextEF.AppliedLoans.RemoveRange(user.AppliedLoans);
                    _dbContextEF.Files.RemoveRange(user.ProfilePicture);
                    _dbContextEF.Users.Remove(user);
                    await _dbContextEF.SaveChangesAsync();
                }
            }
            catch(Exception ex) { }
        }
    }
}
