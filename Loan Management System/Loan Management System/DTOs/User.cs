namespace Loan_Management_System.DTOs
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public decimal Salary { get; set; }
        public string Employer { get; set; }
        public string Designation { get; set; }
        public string UserPic { get; set; }
        public ICollection<AppliedLoan> AppliedLoans { get; set; }
    }
}
