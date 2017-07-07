using System.ComponentModel.DataAnnotations;

namespace AppLunch.Models
{
    public class ResetPasswordModel
    {
        [StringLength(20)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(20)]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword must match.")]
        public string ConfirmPassword { get; set; }

        public string UserID { get; set; }

        public string Token { get; set; }
    }
}