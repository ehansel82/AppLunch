using System.ComponentModel.DataAnnotations;

namespace AppLunch.Models
{
    public class RegistrationModel
    {
        [StringLength(100)]
        public string UserName { get; set; }
        
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}