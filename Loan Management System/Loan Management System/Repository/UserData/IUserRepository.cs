using Loan_Management_System.DTOs;
using Loan_Management_System.Models;

namespace Loan_Management_System.Repository.UserData
{
    public interface IUserRepository
    {
        Task<User> GetUser(LoginFormData credentials);
        Task<User> CreateUser(SignupDTO userData);
        Task DeleteUser(int userId);

    }
}
