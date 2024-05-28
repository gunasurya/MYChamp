using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MYChamp.Models
{
    public class TestAttemptViewModel
    {
        public int TestId { get; set; }
        public int UserId { get; set; }
        public List<QuestionModel> QuestionsList { get; set; }
    }

    public class QuestionModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public List<string> Options { get; set; }  
        [Required(ErrorMessage = "Please select an option")]
        public string SelectedOption { get; set; }
    }

    public class TestAttemptModel
    {
        [Key]
        public int Id { get; set; } // Adding a primary key

        public int TestId { get; set; }
        public int UserId { get; set; }
        public string selectedQuestions { get; set; }
        public string selectedOptions { get; set; }
    }



}
