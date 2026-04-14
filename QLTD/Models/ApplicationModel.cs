using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QLTD.Models
{
    [Table("tblApplication")]
    public class ApplicationModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApplicationID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Column("Email", TypeName = "varchar(max)"), Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Column("CoverLetter", TypeName = "varchar(max)"), Required]
        public string CoverLetter { get; set; }
        public string CVPath { get; set; }
        public string CVFileName { get; set; }
        [Required]
        public string view { get; set; }
        [Required]
        public DateTime ApplicationTime { get; set; }
        public int CandidateID { get; set; }

        [ForeignKey("CandidateID")]
        public CandidateModel Candidate { get; set; }

        public int JobID { get; set; }
        [ForeignKey("JobID")]
        public JobModel Job { get; set; }
    }
}
