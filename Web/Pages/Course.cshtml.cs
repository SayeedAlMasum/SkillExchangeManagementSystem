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

      
    

        public void OnGet()
        {
            // Load courses from the service
            var result = new CourseService().List();
            if (result.Success)
            {
                Courses = (List<Course>)result.Data;
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Course.IsPremium)
                {
                    // Redirect to payment page or handle payment
                    return RedirectToPage("/Payment", new { courseId = Course.CourseId });
                }
                else
                {
                    using (var context = new SkillExchangeContext())
                    {
                        context.Course.Add(Course);
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
