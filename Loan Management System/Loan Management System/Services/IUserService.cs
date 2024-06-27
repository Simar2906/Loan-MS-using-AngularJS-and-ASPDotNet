using Loan_Management_System.DTOs;

namespace Loan_Management_System.Services
{
    public interface IUserService
    {
        Task<string> GetUser(LoginFormData credentials);
        Task<string> CreateUser(SignupDTO userData);
        Task DeleteUser(int userId);
    }
}
