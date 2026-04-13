using System.ComponentModel.DataAnnotations.Schema;

namespace QLTD.Models
{
    [Table("tblCompanys")]
    public class CompanyModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
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
