using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace MYChamp.Models
{

    public class PersonalityQuestionModel

    {
        [Key]
        public int question_id { get; set; }
        public string question_text { get; set; }
        public string options { get; set; }
        public char correct_answer { get; set; }


    }
     
}
