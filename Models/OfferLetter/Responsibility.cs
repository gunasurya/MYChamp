using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MYChamp.Models.OfferLetter
{
    public class Responsibility
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string ResponsibilityText { get; set; }

        [ForeignKey("Position")]
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
