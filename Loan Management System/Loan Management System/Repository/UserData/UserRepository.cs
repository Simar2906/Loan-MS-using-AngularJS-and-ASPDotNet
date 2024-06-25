using Loan_Management_System.Data;
using Loan_Management_System.DTOs;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace Loan_Management_System.Repository.UserData
{
    public class UserRepository:IUserRepository
    {
        private readonly ApplicationDbContext _dbContextEF;
        public UserRepository(ApplicationDbContext dbContextEF) 
        {
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
        public async Task<User> CreateUser(User userData)
        {
            _dbContextEF.Users.Add(userData);
            await _dbContextEF.SaveChangesAsync();
            var user = await _dbContextEF.Users.FirstOrDefaultAsync(u => u.Email == userData.Email && u.Password == userData.Password);
            return user;
        }

        public async Task DeleteUser(int userId)
        {
            var user = await _dbContextEF.Users
                .Include(u => u.AppliedLoans)  // Include loans to delete them too
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                _dbContextEF.AppliedLoans.RemoveRange(user.AppliedLoans);
                _dbContextEF.Users.Remove(user);
                await _dbContextEF.SaveChangesAsync();
            }
        }
    }
}
