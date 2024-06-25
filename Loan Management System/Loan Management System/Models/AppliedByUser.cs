namespace Loan_Management_System.Models
{
    public class AppliedByUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public string UserPicturePath    { get; set; }
        public string LoanLogoPath { get; set; }
        public string Title { get; set; }
        public decimal MinLoanAmount { get; set; }
        public decimal MaxLoanAmount { get; set; }
        public decimal MinInterestRate { get; set; }
        public decimal MaxInterestRate { get; set; }
        public decimal AppliedAmount { get; set; }
        public decimal AppliedRate { get; set; }
        public int MinCreditScore { get; set; }
        public decimal TermLength { get; set; }
        public decimal ProcessingFee { get; set; }
        public string Employer { get; set; }
        public decimal Salary { get; set; }
        public string Designation { get; set; }
        public Status Status { get; set; }
        public DateTime DateApplied { get; set; }
        public int LoanId { get; set; }
    }
}
