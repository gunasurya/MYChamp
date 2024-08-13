using System.ComponentModel.DataAnnotations;

namespace MYChamp.Models.OfferLetter
{
    public class EmployeeProfile
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Position { get; set; }

        [Required]
        public int AllotedLeaves { get; set; }

        [Required]
        public int RemainingLeaves { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}
