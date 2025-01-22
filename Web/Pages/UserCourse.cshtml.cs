//UserCourse.cshtml.cs
using System.Collections.Generic;
using Business.Services;
using Database.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class UserCoursesModel : PageModel
    {
        public List<Course> Courses { get; set; } = new List<Course>();

        public void OnGet()
        {
            // Load courses from the service
            var result = new CourseService().List();
            if (result.Success)
            {
                Courses = (List<Course>)result.Data;
            }
        }
    }
}
