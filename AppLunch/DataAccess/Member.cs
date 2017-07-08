using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppLunch.DataAccess
{
    [Table("Member", Schema = "AppLunch")]
    public class Member
    {
        [Key, Required]
        public int ID { get; set; }

        [StringLength(50), Required]
        public string FirstName { get; set; }

        [StringLength(50), Required]
        public string LastName { get; set; }

        [StringLength(128), Required]
        public string IdentityID { get; set; }
    }
}