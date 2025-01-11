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
        public Course NewCourse { get; set; } = new Course();

        [BindProperty]
        public string SelectedCategory { get; set; } = string.Empty;

        public void OnGet()
        {
            // Load courses from the service
            var result = new CourseService().List();
            if (result.Success)
            {
                Courses = (List<Course>)result.Data;
            }
        }

        public IActionResult OnPostAddCourse()
        {
            if (ModelState.IsValid)
            {
                if (NewCourse.IsPremium)
                {
                    // Redirect to payment page or handle payment
                    return RedirectToPage("/Payment", new { courseId = NewCourse.CourseId });
                }
                else
                {
                    using (var context = new SkillExchangeContext())
                    {
                        context.Course.Add(NewCourse);
                        context.SaveChanges();
                    }
                    return RedirectToPage();
                }
            }

            // Reload courses in case of error
            OnGet();
            return Page();
        }

    }
}
