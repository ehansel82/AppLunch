using System.ComponentModel.DataAnnotations;

namespace AppLunch.Models
{
    public class LoginModel
    {
        [StringLength(256)]
        [Required]
        [EmailAddress]
        public string UserName { get; set; }

        [StringLength(20)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}