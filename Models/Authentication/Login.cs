using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MYChamp.Models.Authentication
{
    public class Login
    {
        [Required(ErrorMessage = "Username is requried")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Password is Requried")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DisplayName("Remember me")]
        public bool RememberMe { get; set; }
    }
}
