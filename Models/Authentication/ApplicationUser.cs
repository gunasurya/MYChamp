using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MYChamp.Models.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? Firstname { get; set; }

        [Required]
        public string? Middlename { get; set; }

        [Required]
        public string? Lastname { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string? Address { get; set; }
    }
}
