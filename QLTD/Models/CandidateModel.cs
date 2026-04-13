using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTD.Models
{
    [Table("tblCandidates")]
    public class CandidateModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CandidateID { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Column("Password", TypeName = "varchar(max)"), Required]
        public string Password { get; set; }
        [Column("Email", TypeName = "varchar(max)"), Required]
        public string Email { get; set; }
        [Required]
        public Boolean Status { get; set; }
    }
}
