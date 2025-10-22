using System.ComponentModel.DataAnnotations;

namespace Contract_Monthly_Claims.Models
{
    public class Lecturer
    {
        public int LecturerId { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public string? Department { get; set; }
    }
}
