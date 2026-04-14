using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QLTD.Models
{
    [Table("tblUsers")]
    public class UserModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Column("Password", TypeName = "varchar(max)"), Required]
        public string Password { get; set; } = string.Empty;

        [Column("Email", TypeName = "varchar(max)"), Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Position { get; set; } = string.Empty;

        [Required]
        public bool Status { get; set; }

        [Required]
        public int PermissionID { get; set; }

        [ForeignKey("PermissionID")]
        public PermissionModel? Permission { get; set; }

        public int CompanyID { get; set; }

        [ForeignKey("CompanyID")]
        public CompanyModel? Company { get; set; }
    }
}
