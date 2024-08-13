using System.ComponentModel.DataAnnotations;

namespace MYChamp.Models.OfferLetter
{
    public class Position
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Position Name is required")]
        [MaxLength(100, ErrorMessage = "Position Name cannot be longer than 100 characters")]
        public string PositionName { get; set; }

        [Required(ErrorMessage = "Level is required")]
        [MaxLength(50, ErrorMessage = "Level cannot be longer than 50 characters")]
        public string Level { get; set; }

        [Required(ErrorMessage = "Alloted Leaves is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Alloted Leaves must be greater than 0")]
        public int AllotedLeaves { get; set; }

        [Required(ErrorMessage = "Number of Leaves is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of Leaves must be greater than 0")]

        public int NumberOfLeaves { get; set; }

        public List<Responsibility> Responsibilities { get; set; }
    }
}
