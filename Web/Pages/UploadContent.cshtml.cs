//UploadContent.cshtml.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Database.Model;
using Business.Services;
using System.IO;

namespace Web.Pages
{
    [Authorize(Roles = "Teacher")]
    public class UploadContentModel : PageModel
    {
        [BindProperty]
        public Content ContentData { get; set; } = new Content(); // Renamed to ContentData

        [BindProperty]
        public IFormFile File { get; set; }

        [BindProperty]
        public int CourseId { get; set; }     

        public Course Course { get; set; } = new Course();

        public IActionResult OnGet(int courseId)
        {
            CourseId = courseId;
            var result = new CourseService().GetCourseById(courseId);
            if (result.Success && result.Data is Course course)
            {
                Course = course;
            }
            else
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Save the uploaded file to a specific location
            if (File != null && File.Length > 0)
            {
                var filePath = Path.Combine("wwwroot/uploads", File.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    File.CopyTo(stream);
                }

                // Set the URL of the content to the file path
                ContentData.URL = $"/uploads/{File.FileName}";
            }

            // Associate the content with the course
            ContentData.CourseId = CourseId;

            // Save the content to the database
            var result = new ContentService().AddContent(ContentData);
            if (result.Success)
            {
                return RedirectToPage("/Content", new { courseId = CourseId });
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return Page();
            }
        }
    }
}
