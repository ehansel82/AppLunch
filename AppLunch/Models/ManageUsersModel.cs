using AppLunch.DataAccess;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppLunch.Models
{
    [NotMapped]
    public class ManageUsersModel : Member
    {
        public bool IsManager { get; set; }
    }
}