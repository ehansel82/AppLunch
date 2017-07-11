using System.ComponentModel.DataAnnotations;

namespace AppLunch.Models
{
    public class InviteModel
    {
        [Required, EmailAddress, StringLength(256)]
        public string Invitee { get; set; }

        [Required, EmailAddress, StringLength(256)]
        public string Inviter { get; set; }
    }
}