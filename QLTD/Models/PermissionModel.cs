using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QLTD.Models
{
    [Table("tblPermissions")]
    public class PermissionModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PermissionID { get; set; }

        [Required]
        public string Role { get; set; }

        public ICollection<UserModel> Users { get; set; } = new List<UserModel>();
    }
}
