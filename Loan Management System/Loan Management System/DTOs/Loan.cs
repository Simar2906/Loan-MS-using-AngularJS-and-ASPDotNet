using System.ComponentModel.DataAnnotations.Schema;

namespace Loan_Management_System.DTOs
{
    public class Loan
    {
        public int Id { get; set; }

        [Column(TypeName = "character varying(40)")]
        public string Title { get; set; }

        public int MinCreditScore { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TermLength { get; set; }

        [Column(TypeName = "numeric(18,2)")]
        public decimal ProcessingFee { get; set; }

        public int LogoFileId { get; set; }
        
        [ForeignKey(nameof(LogoFileId))]
        public File LogoPicture { get; set; }

        [Column(TypeName = "numeric(18,2)")]
        public decimal MinLoanAmount { get; set; }
        
        [Column(TypeName = "numeric(18,2)")]
        public decimal MaxLoanAmount { get; set; }

        [Column(TypeName = "numeric(5,2)")]
        public decimal MinInterestRate { get; set; }
        
        [Column(TypeName = "numeric(5,2)")]
        public decimal MaxInterestRate { get; set; }

        public ICollection<AppliedLoan> AppliedLoans { get; set; }

    }
}
