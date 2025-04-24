// Course.cshtml.cs
using System.Collections.Generic;
using Business.Services;
using Database.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class CourseModel : PageModel
    {
        private readonly CourseService _courseService;

        public CourseModel()
        {
            _courseService = new CourseService();
        }

        public List<Course> Courses { get; set; } = new List<Course>();

        [BindProperty]
        public Course Course { get; set; } = new Course();

        public void OnGet()
        {
            var result = _courseService.List();
            if (result.Success)
            {
                Courses = (List<Course>)result.Data;
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Log validation errors
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return Page();
            }

            // Log the course data before saving
            Console.WriteLine($"Title: {Course.Title}, Description: {Course.Description}, Category: {Course.Category}, SubCategory: {Course.SubCategory}, IsPremium: {Course.IsPremium}");

            // Set the CreatedBy field to the current user's name
            Course.CreatedBy = User.Identity?.Name ?? "System";

            var result = _courseService.AddCourse(Course);
            if (result.Success)
            {
                return RedirectToPage("/Admin/Course");
            }

            // Log the error message
            Console.WriteLine(result.Message);

            ModelState.AddModelError("", result.Message);
            OnGet();
            return Page();
        }
        public IActionResult OnPostUpdate()
        {
            if (ModelState.IsValid)
            {
                // Set the UpdatedBy field to the current user's name
                Course.UpdatedBy = User.Identity?.Name ?? "System";

                var result = _courseService.UpdateCourse(Course);
                if (result.Success)
                {
                    return RedirectToPage("/Admin/Course");
                }
                ModelState.AddModelError("", result.Message);
            }
            OnGet();
            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            var result = _courseService.DeleteCourse(id);
            if (result.Success)
            {
                return RedirectToPage("/Admin/Course");
            }
            ModelState.AddModelError("", result.Message);
            OnGet();
            return Page();
        }

    }
}