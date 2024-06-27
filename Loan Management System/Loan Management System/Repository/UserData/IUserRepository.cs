using Loan_Management_System.DTOs;
using Loan_Management_System.Models;

namespace Loan_Management_System.Repository.UserData
{
    public interface IUserRepository
    {
        Task<User> GetUser(LoginFormData credentials);
        Task<Models.File> SaveFile(Models.File profilePicture);
        Task<User> AddUser(SignupDTO userData, string hashedPassword, string salt, int profilePictureId);
        Task DeleteUser(int userId);

    }
}
