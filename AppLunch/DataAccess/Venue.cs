using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppLunch.DataAccess
{
    [Table("Venue", Schema = "AppLunch")]
    public class Venue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(50), Required, Column(TypeName = "VARCHAR")]
        public string Name { get; set; }

        [StringLength(50), Column(TypeName = "VARCHAR")]
        public string Description { get; set; }

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

        [Column(TypeName = "VARCHAR")]
        public string UpadateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public ICollection<Location> Locations { get; set; }
    }
}