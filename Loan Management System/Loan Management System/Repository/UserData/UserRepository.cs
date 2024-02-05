using Loan_Management_System.Data;
using Loan_Management_System.DTOs;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;

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
            var user = await _dbContextEF.Users.FirstOrDefaultAsync(u => u.Email == credentials.email && u.Password == credentials.password);
            return user;
        }
    }
}
