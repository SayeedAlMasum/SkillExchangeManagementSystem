using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class Course:BaseModel
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;
        [Required]
        public string SubCategory { get; set; } = string.Empty;

        [Required]
        public bool IsPremium { get; set; }
    }
}
