using System.ComponentModel.DataAnnotations;


namespace Contract_Monthly_Claims.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }

        [Required]
        public int LecturerId { get; set; }

        public Lecturer? Lecturer { get; set; }

        [Required(ErrorMessage = "Hours worked is required.")]
        [Range(1, 200, ErrorMessage = "Hours worked must be between 1 and 200.")]
        public double HoursWorked { get; set; }

        [Required(ErrorMessage = "Hourly rate is required.")]
        [Range(100, 2000, ErrorMessage = "Hourly rate must be between 100 and 2000.")]
        public double HourlyRate { get; set; }

        public string? Notes { get; set; }

        public double TotalAmount => HoursWorked * HourlyRate;

        public string Status { get; set; } = "Pending"; // Pending / Verified / Approved / Rejected
        public DateTime DateSubmitted { get; set; } = DateTime.Now;

        public string CurrentApprover { get; set; } = "Programme Coordinator";


        public List<SupportingDocument> SupportingDocuments { get; set; } = new();
    }
}
