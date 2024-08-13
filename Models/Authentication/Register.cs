using System.ComponentModel.DataAnnotations;

namespace MYChamp.Models.Authentication
{
    public class Register
    {

        [Required(ErrorMessage = "First name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name should contain only alphabetic characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Middle name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Middle name should contain only alphabetic characters")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name should contain only alphabetic characters")]
        public string LastName { get; set; }
        [Required]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
    }
}
