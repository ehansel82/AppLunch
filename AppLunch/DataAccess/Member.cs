using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppLunch.DataAccess
{
    [Table("Member", Schema = "AppLunch")]
    public class Member
    {
        public Member()
        {
            Rankings = new List<Ranking>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(50), Required, Column(TypeName = "VARCHAR")]
        public string FirstName { get; set; }

        [StringLength(50), Required, Column(TypeName = "VARCHAR")]
        public string LastName { get; set; }

        [StringLength(128), Required, Column(TypeName = "VARCHAR")]
        public string IdentityID { get; set; }

        public ICollection<Ranking> Rankings { get; set; }
    }
}