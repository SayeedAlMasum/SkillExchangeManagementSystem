// Course.cshtml.cs
using System.Collections.Generic;
using Business.Services;
using Database.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
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
            // Check if the model is valid
            if (ModelState.IsValid)
            {
                // Call the AddCourse method from _courseService and pass the Course object
                var result = _courseService.AddCourse(Course);

                // If the result is successful, redirect to the Course page
                if (result.Success)
                {
                    return RedirectToPage("/Course");
                }

                // If there is an error, add the error message to the ModelState
                ModelState.AddModelError("", result.Message);
            }

            // Re-render the page in case of failure or invalid model
            OnGet();

            // Return the current page
            return Page();
        }


        public IActionResult OnPostUpdate()
        {
            if (ModelState.IsValid)
            {
                var result = _courseService.UpdateCourse(Course);
                if (result.Success)
                {
                    return RedirectToPage("/Course");
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
                return RedirectToPage("/Course");
            }
            ModelState.AddModelError("", result.Message);
            OnGet();
            return Page();
        }
    }
}
