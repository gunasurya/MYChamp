using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MYChamp.AuthModel
{
    public class ConsulteeLog
    {
        [Key]
        [Column("logid")]
        public int LogId { get; set; }

        [Column("consulteeid")]
        [Required(ErrorMessage = "Select the consultee")]
        public int ConsulteeId { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "From DateTime is required")]
        [Column("fromdatetime")]
        public DateTime FromDateTime { get; set; }

        [Required(ErrorMessage = "To DateTime is required")]
        [Column("todatetime")]
        public DateTime ToDateTime { get; set; }

        [Column("duration")]
        public double Duration { get; set; }

        [Column("cost")]
        public double Cost { get; set; }

        [Required(ErrorMessage = "Generate Report is required")]
        [Column("generatereport")]
        public string GenerateReport { get; set; } = "Generate";
    }
}
