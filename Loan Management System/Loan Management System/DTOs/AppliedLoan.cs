namespace Loan_Management_System.DTOs
{
    public class AppliedLoan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AppliedAmount { get; set; }
        public float AppliedRate { get; set; }
        public int TermLength { get; set; }
        public int Status { get;set; } //1 - pending, 2 - approved, 3 - rejected
        public DateTime DateApplied { get; set; }
        public int LoanId { get; set; }
        //nav-prop
        public Loan? Loan { get; set; }
        public User? User { get; set; }
    }
}
