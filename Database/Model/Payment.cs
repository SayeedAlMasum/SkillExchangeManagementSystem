using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        public string? CourseId { get; set; }

        [Required]
        public string UserInfoId { get; set; }

        [Required]
        public string PaymentStatus { get; set; } = "Pending";

        public string? CardNumber { get; set; }
        public string? ExpiryDate { get; set; }
        public string? CVV { get; set; }
    }
}
