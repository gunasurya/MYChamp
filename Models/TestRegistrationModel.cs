using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MYChamp.Models
{
    public class TestRegistrationModel
    {
        [Key]
        public int testid { get; set; }


        [Required(ErrorMessage = "Enter value")]
        public string name { get; set; }

        [Required(ErrorMessage = "Enter value")]
        public int timer { get; set; }

        [Required(ErrorMessage = "Enter value")]
        public string description { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Upload Image")]
        public IFormFile ImageFile { get; set; }

        public string imagename { get; set; } = string.Empty;
        public string imagebase64 { get; set; } = string.Empty;
         

    }
   
}
