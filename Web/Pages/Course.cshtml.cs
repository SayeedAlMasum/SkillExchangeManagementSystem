// Course.cshtml.cs
using System.Collections.Generic;
using Business.Services;
using Database.Context;
using Database.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class CourseModel : PageModel
    {
        public List<Course> Courses { get; set; } = new List<Course>();

        [BindProperty]
        public Course Course { get; set; } = new Course();

        // OnGet method to load all courses for displaying in the list
        public void OnGet()
        {
            var result = new CourseService().List();  // Get courses from the service
            if (result.Success)
            {
                Courses = (List<Course>)result.Data;
            }
        }

        // OnPost method for adding a new course
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Set the course creation date (optional)
                Course.CreatedDate = DateTime.Now;

                // Save the course to the database (both premium and non-premium)
                using (var context = new SkillExchangeContext())
                {
                    context.Course.Add(Course);  // Add course to the database
                    context.SaveChanges();  // Save changes to the database
                }

                return RedirectToPage("/Course");  // Redirect to the course page after saving
            }

            // If model state is invalid, reload the page with the errors
            OnGet();
            return Page();
        }
        // OnPost method for updating an existing course
        public IActionResult OnPostUpdate()
        {
            if (ModelState.IsValid)
            {
                var result = new CourseService().UpdateCourse(Course);
                if (result.Success)
                {
                    return RedirectToPage("/Course"); // Redirect to the course list after updating
                }

                // Handle update failure (e.g., display an error message)
                ModelState.AddModelError("", result.Message);
            }

            // If the model state is invalid, reload the page with errors
            OnGet();
            return Page();
        }

        // OnPost method for deleting a course
        public IActionResult OnPostDelete(int courseId)
        {
            var result = new CourseService().DeleteCourse(courseId);
            if (result.Success)
            {
                return RedirectToPage("/Course"); // Redirect to the course list after deletion
            }

            // Handle deletion failure (e.g., display an error message)
            ModelState.AddModelError("", result.Message);
            OnGet();
            return Page();
        }
    }
}