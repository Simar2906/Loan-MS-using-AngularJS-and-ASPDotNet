using Loan_Management_System.Models;

namespace Loan_Management_System.DTOs
{
    public class SignupDTO
    {
        public string Designation { get; set; }
        public string Email { get; set; }
        public string Employer { get; set; }
        public string FileName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public decimal Salary { get; set; }
        public string UserPic { get; set; }
    }
}
