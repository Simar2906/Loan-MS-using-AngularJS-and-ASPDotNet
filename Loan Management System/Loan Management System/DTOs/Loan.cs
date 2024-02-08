namespace Loan_Management_System.DTOs
{
    public class Loan
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        public string Title { get; set; }
        public string LoanAmount { get; set; }
        public string InterestRates { get; set; }
        public int MinCreditScore { get; set; }
        public decimal TermLength { get; set; }
        public decimal ProcessingFee { get; set; }

    }
}
