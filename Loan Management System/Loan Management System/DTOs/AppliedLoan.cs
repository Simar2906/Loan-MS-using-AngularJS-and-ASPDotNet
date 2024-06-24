using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loan_Management_System.Models;

namespace Loan_Management_System.DTOs
{
    public class AppliedLoan
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }

        [Column(TypeName = "numeric(18, 2)")]
        public decimal AppliedAmount { get; set; }

        [Column(TypeName = "numeric(5, 2)")]
        public decimal AppliedRate { get; set; }

        public int TermLength { get; set; }
        public Status Status { get;set; } //1 - pending, 2 - approved, 3 - rejected
        public DateTime DateApplied { get; set; }
        public int LoanId { get; set; }

        [ForeignKey(nameof(LoanId))]    
        public Loan? Loan { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
    }
}
