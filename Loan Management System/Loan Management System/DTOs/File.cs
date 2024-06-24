using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loan_Management_System.DTOs
{
    public class File
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "character varying(255)")]
        public string FilePath { get; set; }

        // Optional: Foreign key to User (one-to-one)
        public User User { get; set; }

        // Navigation property for one-to-one relationship with Loan
        public Loan Loan { get; set; }
    }
}
