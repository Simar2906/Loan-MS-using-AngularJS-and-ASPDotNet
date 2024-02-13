namespace Loan_Management_System.Models
{
    public class AppliedByUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string UserPic { get; set; }
        public string Logo { get; set; }
        public string Title { get; set; }
        public string LoanAmount { get; set; }
        public string InterestRates { get; set; }
        public int AppliedAmount { get; set; }
        public float AppliedRate { get; set; }
        public int MinCreditScore { get; set; }
        public int TermLength { get; set; }
        public decimal ProcessingFee { get; set; }
        public string Employer { get; set; }
        public decimal Salary { get; set; }
        public string Designation { get; set; }
        public int Status { get; set; }
        public DateTime DateApplied { get; set; }
        public int LoanId { get; set; }
    }
}
