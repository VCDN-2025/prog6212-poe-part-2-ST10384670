using System.ComponentModel.DataAnnotations;

namespace Contract_Monthly_Claims.Models
{
    public class ProgrammeCoordinator
    {
        public int CoordinatorId { get; set; }

        [Required]
        public string? FullName { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        public List<Claim> VerifiedClaims { get; set; } = new();
    }
}
