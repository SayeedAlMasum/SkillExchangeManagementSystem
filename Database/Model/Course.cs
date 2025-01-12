using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class Course : BaseModel
    {
        [Key]
        public int CourseId { get; set; }   // Automatically generating a new GUID

        [Required]
        public string Title { get; set; }  // Non-nullable, so ensure that it's set when creating a course

        [Required]
        public string Description { get; set; }  // Non-nullable, so ensure that it's set when creating a course

        [Required]
        public string Category { get; set; }  // Non-nullable, so ensure that it's set when creating a course

        [Required]
        public bool IsPremium { get; set; }  // Non-nullable, indicating if it's a paid course

        public string? SubCategory { get; set; }  // Nullable, as not all courses may have a subcategory
    }
}
