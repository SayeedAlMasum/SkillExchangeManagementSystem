//Content.cs
using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class Content
    {
        public int ContentId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        public string URL { get; set; }

        [Required(ErrorMessage = "Please select a course")]
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        public DateTime CreatedDate { get; set; }

        // Navigation property
        public Course Course { get; set; }
    }
}