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
            return user;
        }
        public async Task<DTOs.File> SaveFile(DTOs.File profilePicture)
        {
            _dbContextEF.Files.Add(profilePicture);
            await _dbContextEF.SaveChangesAsync();
            return profilePicture;
        }
        public async Task<User> AddUser(SignupDTO userData, string hashedPassword, string salt, int profilePictureId)
        {
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
            string sql = @"
        INSERT INTO ""Users"" (""Designation"", ""Email"", ""Employer"", ""FirstName"", ""LastName"", ""Password"", ""Gender"", ""Role"", ""Salary"", ""Salt"", ""ProfilePictureFileId"")
        VALUES (@Designation, @Email, @Employer, @FirstName, @LastName, @Password, @Gender::""Gender"", @Role::""Role"", @Salary, @Salt, @ProfilePictureFileId)"; // If using SQL Server to get the last inserted id
            _db.Execute(sql, parameters);
            var user = await _dbContextEF.Users.FirstOrDefaultAsync(u => u.Email == userData.Email && u.Password == hashedPassword);
            return user;
        }
        
        public async Task DeleteUser(int userId)
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
    }
}
