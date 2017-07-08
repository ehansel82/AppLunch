using System.ComponentModel.DataAnnotations;

namespace AppLunch.Models
{
    public class ResetPasswordModel
    {
        [StringLength(20)]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [StringLength(20)]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword must match.")]
        [Display(Name="Confirm New Password")]
        public string ConfirmNewPassword { get; set; }

        public string UserID { get; set; }

        public string Token { get; set; }
    }
}