using System.ComponentModel.DataAnnotations;

namespace Contract_Monthly_Claims.Models
{
    public class SupportingDocument
    {
        public int DocumentId { get; set; }

        [Required]
        public string? FileName { get; set; }

        [Required]
        public string? FilePath { get; set; }

        public long FileSize { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.Now;

        [Required]
        public int ClaimId { get; set; }
    }
}
