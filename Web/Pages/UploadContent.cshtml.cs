//UploadContent.cshtml.cs
using Business.Services;
using Database.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Web.Pages.Content
{
    [Authorize(Policy = "TeacherOnly")]
    public class UploadContentModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly CourseService _courseService;
        private readonly ContentService _contentService;

        public UploadContentModel(IWebHostEnvironment environment)
        {
            _environment = environment;
            _courseService = new CourseService();
            _contentService = new ContentService();
        }

        [BindProperty]
        public Database.Model.Content Content { get; set; } = new Database.Model.Content();

        [BindProperty]
        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "Please select a file to upload")]
        public IFormFile UploadFile { get; set; }

        public List<SelectListItem> CourseOptions { get; set; } = new List<SelectListItem>();
        public List<Course> TeacherCourses { get; set; } = new List<Course>();
        public string Message { get; set; } = "";

        public void OnGet()
        {
            LoadCourses();
            LoadTeacherCourses();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            LoadCourses();
            LoadTeacherCourses();

            if (!ModelState.IsValid)
            {
                Message = "Please fix the errors.";
                return Page();
            }

            // Validate file type
            var allowedExtensions = new[] { ".pdf", ".doc", ".docx", ".ppt", ".pptx", ".mp4" };
            var fileExtension = Path.GetExtension(UploadFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError("UploadFile", "Only PDF, Word, PowerPoint, and MP4 files are allowed.");
                Message = "Please fix the errors.";
                return Page();
            }

            // Get logged-in teacher's email
            var teacherEmail = User.Identity?.Name ?? throw new InvalidOperationException("User not authenticated");

            // Validate the course belongs to the teacher
            if (!TeacherCourses.Any(c => c.CourseId == Content.CourseId))
            {
                ModelState.AddModelError("Content.CourseId", "You can only upload content to your own courses");
                Message = "Please fix the errors.";
                return Page();
            }

            try
            {
                // Save file to wwwroot/uploads/{email}/
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", teacherEmail);
                Directory.CreateDirectory(uploadsFolder);

                // Generate unique filename to prevent overwrites
                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await UploadFile.CopyToAsync(stream);
                }

                // Save file URL (relative path)
                Content.URL = $"/uploads/{teacherEmail}/{fileName}";

                var result = _contentService.AddContent(Content);
                if (result.Success)
                {
                    Message = "Content uploaded successfully.";
                    ModelState.Clear();
                    Content = new Database.Model.Content();
                    UploadFile = null;
                }
                else
                {
                    Message = $"Upload failed: {result.Message}";
                    // Clean up the uploaded file if content creation failed
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                Message = $"An error occurred while uploading: {ex.Message}";
                ModelState.AddModelError("", Message);
            }

            return Page();
        }

        private void LoadCourses()
        {
            var teacherEmail = User.Identity?.Name ?? "";
            var result = _courseService.ListByTeacher(teacherEmail);

            if (result.Success && result.Data is List<Course> courses)
            {
                CourseOptions = courses
                    .Select(c => new SelectListItem
                    {
                        Value = c.CourseId.ToString(),
                        Text = $"{c.Title} ({c.Category})"
                    })
                    .ToList();

                // Preselect if only one course exists
                if (CourseOptions.Count == 1)
                {
                    Content.CourseId = int.Parse(CourseOptions[0].Value);
                }
            }
            else
            {
                Message = result.Success ? "No courses available" : "Failed to load courses";
                CourseOptions = new List<SelectListItem>();
            }
        }

        private void LoadTeacherCourses()
        {
            var teacherEmail = User.Identity?.Name ?? "";
            var result = _courseService.ListByTeacher(teacherEmail);

            if (result.Success && result.Data is List<Course> courses)
            {
                TeacherCourses = courses;
            }
            else
            {
                TeacherCourses = new List<Course>();
            }
        }
    }
}