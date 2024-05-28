using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MYChamp.AuthModel
{
    public class ConsulteeRegister
    {
        [Key]
        [Column("consulteeid")]
        public int consulteeId { get; set; }

        [Required(ErrorMessage = "Consultee Name is required")]
        [Column("consulteename")]
        public string consulteeName { get; set; } 

        [Required(ErrorMessage = "Consultee Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Column("consulteeemail")]
        public string consulteeEmail { get; set; }

        [Required(ErrorMessage = "Currency Unit is required")]
        [Column("unitofcurrency")]
        public string UnitOfCurrency { get; set; }

        [Required(ErrorMessage = "Cost Per Hour is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid cost")]
        [Column("costperhour")]
        public double? costPerHour { get; set; }

        [Required(ErrorMessage = "Billing Date is required")]
        [DataType(DataType.Date)]
        [Column("billingdate")]
        public DateTime? BillingDate { get; set; }
    }
}
