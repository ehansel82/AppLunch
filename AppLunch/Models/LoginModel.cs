﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace AppLunch.Models
{
    [DataContract]
    public class LoginModel
    {
        [DataMember]
        [StringLength(256)]
        [Required]
        [EmailAddress]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [DataMember]
        [StringLength(20)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [IgnoreDataMember]
        public bool isRegistrationRedirect { get; set; }

        [IgnoreDataMember]
        public bool isPasswordRedirect { get; set; }

        [IgnoreDataMember]
        public bool needsEmailConfirmed { get; set; }

        [IgnoreDataMember]
        public bool suggestPasswordReset { get; set; }
    }
}