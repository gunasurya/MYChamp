using System.ComponentModel.DataAnnotations;

namespace MYChamp.Models.OfferLetter
{
    public class Employee
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "The Employee Name field is required.")]
        [StringLength(100, ErrorMessage = "The Employee Name must be at most 100 characters long.")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "The Position field is required.")]
        [StringLength(50, ErrorMessage = "The Position must be at most 50 characters long.")]
        public string Position { get; set; }

        [Required(ErrorMessage = "The Date of Joining field is required.")]
        [DataType(DataType.Date)]
        public DateTime DateOfJoining { get; set; }

        [Required(ErrorMessage = "The Work Location field is required.")]
        [StringLength(100, ErrorMessage = "The Work Location must be at most 100 characters long.")]
        public string WorkLocation { get; set; }

        [Required(ErrorMessage = "The Reporting Manager Name field is required.")]
        [StringLength(100, ErrorMessage = "The Reporting Manager Name must be at most 100 characters long.")]
        public string ReportingManagerName { get; set; }

        [Required(ErrorMessage = "The Job Type field is required.")]
        public string JobType { get; set; }

        [Required(ErrorMessage = "The Working Days field is required.")]
        [Range(1, 7, ErrorMessage = "The Working Days must be between 1 and 7.")]
        public int WorkingDays { get; set; }

        [Required(ErrorMessage = "The Working Hours field is required.")]
        [Range(1, 24, ErrorMessage = "The Working Hours must be between 1 and 24.")]
        public int WorkingHours { get; set; }

        [Required(ErrorMessage = "The Offer Letter Code field is required.")]
        [StringLength(50, ErrorMessage = "The Offer Letter Code must be at most 50 characters long.")]
        public string OfferLetterCode { get; set; }

        [Required(ErrorMessage = "The Date of Offering field is required.")]
        [DataType(DataType.Date)]
        public DateTime DateOfOffering { get; set; }

        [Required(ErrorMessage = "The Status field is required.")]
        [StringLength(20, ErrorMessage = "The Status must be at most 20 characters long.")]
        public string Status { get; set; } = "Pending";
    }
}
