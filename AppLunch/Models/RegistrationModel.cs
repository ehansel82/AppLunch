using System;
using System.ComponentModel.DataAnnotations;

namespace AppLunch.Models
{
    public class RegistrationModel
    {
        [StringLength(256)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(20)]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword must match.")]
        public string ConfirmPassword { get; set; }

        [StringLength(20)]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(20)]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public Guid InviteToken { get; set; }
    }
}