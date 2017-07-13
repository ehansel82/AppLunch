using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppLunch.DataAccess
{
    [Table("Location", Schema = "AppLunch")]
    public class Location
    {
        [Key, Required]
        public int ID { get; set; }

        [StringLength(50), Required, Column(TypeName = "VARCHAR")]
        public string Name { get; set; }

        [StringLength(50), Column(TypeName = "VARCHAR")]
        public string Address { get; set; }

        [StringLength(50), Column(TypeName = "VARCHAR")]
        public string City { get; set; }

        [StringLength(2), Column(TypeName = "VARCHAR")]
        public string State { get; set; }

        [StringLength(5), Column(TypeName = "VARCHAR")]
        public string Zip { get; set; }

        [Required, Column(TypeName = "VARCHAR")]
        public string CreateBy { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
    }
}