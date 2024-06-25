using System.ComponentModel.DataAnnotations.Schema;

namespace Loan_Management_System.DTOs
{
    public class LoanDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int MinCreditScore { get; set; }
        public decimal TermLength { get; set; }
        public decimal ProcessingFee { get; set; }
        public string LogoFilePath { get; set; }
        public decimal MinLoanAmount { get; set; }
        public decimal MaxLoanAmount { get; set; }
        public decimal MinInterestRate { get; set; }
        public decimal MaxInterestRate { get; set; }
    }
}
