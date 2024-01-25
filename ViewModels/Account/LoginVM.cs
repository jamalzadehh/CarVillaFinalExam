

using System.ComponentModel.DataAnnotations;

namespace Carvilla.ViewModels.Account
{
    public class LoginVM
    {
        [Required]
        [MaxLength(255)]
        public string UsernameOrEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public bool IsRemembered { get; set; }
    }
}
