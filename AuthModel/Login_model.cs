using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MYChamp.AuthModel
{
    public class Login_model
    {
        [Required(ErrorMessage ="Username is requried")]

        public string? Name { get; set; }
        [Required(ErrorMessage ="Password is Requried")]
        [DataType(DataType.Password)]

        public string? Password { get; set; }

        [DisplayName("Remember me")]
        public bool RememberMe { get; set; }


    }
}
