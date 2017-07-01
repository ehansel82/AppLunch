using System.ComponentModel.DataAnnotations;

namespace AppLunch.Models
{
    public class RegistrationModel
    {
        [StringLength(256)]
        [Required]
        [EmailAddress]
        public string UserName { get; set; }
        
        [StringLength(20)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(20)]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password and ConfirmPassword must match.")]
        public string ConfirmPassword { get; set; }
    }
}