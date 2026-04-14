using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QLTD.Models
{
    public class JobModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobID { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Jobposition { get; set; }
        [Required]
        public string Cash { get; set; }
        [Required]
        public string Experien { get; set; }
        [Column("Description", TypeName = "varchar(max)"), Required]
        public string Description { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EntimeTime { get; set; }
        [Required]
        public string RequimentEducation { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string WorkingStyle { get; set; }
        public int CompanyID { get; set; }

        [ForeignKey("CompanyID")]
        public CompanyModel Company { get; set; }
        public ICollection<ApplicationModel> Applications { get; set; } = new List<ApplicationModel>();
    }
}
