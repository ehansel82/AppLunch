﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppLunch.DataAccess
{
    [Table("Invite", Schema = "AppLunch")]
    public class Invite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        [EmailAddress, StringLength(256)]
        public string InviteeEmail { get; set; }

        [StringLength(256)]
        public string Inviter { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
    }
}