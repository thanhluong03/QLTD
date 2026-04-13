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
        public string FullName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Column("Password", TypeName = "varchar(max)"), Required]
        public string Password { get; set; }
        [Column("Email", TypeName = "varchar(max)"), Required]
        public string Email { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public Boolean Status { get; set; }
    }
}
