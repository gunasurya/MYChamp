using System.ComponentModel.DataAnnotations;

namespace MYChamp.AuthModel
{
    public class Register_Model
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage ="Passwords are not matching")]
        public string ConfirmPassword {  get; set; }
        [Required]
        public string Address {  get; set; }

    }
}
