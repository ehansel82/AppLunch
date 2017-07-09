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

        [StringLength(50), Required]
        public string Name { get; set; }

        [Required]
        public string CreateBy { get; set; }

        public DateTime CreateDate { get; set; }
    }
}