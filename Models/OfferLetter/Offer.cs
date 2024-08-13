using System.ComponentModel.DataAnnotations;

namespace MYChamp.Models.OfferLetter
{
    public class Offer
    {
        public int Id { get; set; }

        [Required]
        public string OfferLetterCode { get; set; }

        [Required]
        public DateTime DateOfJoining { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}
