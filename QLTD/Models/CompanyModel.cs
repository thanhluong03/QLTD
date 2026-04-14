using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTD.Models
{
    [Table("tblCompanys")]
    public class CompanyModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyID { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Column("Email", TypeName = "varchar(max)"), Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Scalecompany { get; set; }
        [Required]
        public Boolean Status { get; set; }

        public ICollection<UserModel> Users { get; set; } = new List<UserModel>();
        public ICollection<JobModel> Jobs { get; set; } = new List<JobModel>();
    }
}
