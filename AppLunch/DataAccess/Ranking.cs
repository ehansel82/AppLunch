using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppLunch.DataAccess
{
    [Table("Ranking", Schema = "AppLunch")]
    public class Ranking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public Decimal Stars { get; set; }

        public Member Member { get; set; }

        public Venue Venue { get; set; }
    }
}