using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loan_Management_System.Models;

namespace Loan_Management_System.DTOs
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "character varying(255)")]
        public string Email { get; set; }

        [Column(TypeName = "Gender")]
        public Gender Gender { get; set; }

        [Column(TypeName = "character varying(50)")]
        public string FirstName { get; set; }

        [Column(TypeName = "character varying(50)")]
        public string? LastName { get; set; }

        [Column(TypeName = "character varying(128)")]
        public string Password { get; set; }

        [Column(TypeName = "character varying(60)")]
        public string Salt { get; set; }

        [Column(TypeName = "Role")]
        public Role Role { get; set; }

        [Column(TypeName = "numeric(18, 2)")]
        public decimal Salary { get; set; }

        [Column(TypeName = "character varying(40)")]
        public string Employer { get; set; }

        [Column(TypeName = "character varying(40)")]
        public string Designation { get; set; }

        public int ProfilePictureFileId { get; set; }

        [ForeignKey(nameof(ProfilePictureFileId))]
        public File ProfilePicture { get; set; }

        public ICollection<AppliedLoan>? AppliedLoans { get; set; }
    }
}
