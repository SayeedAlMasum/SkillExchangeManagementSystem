//Content.cshmtl.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Business.Services;
using Database.Model;
using System;

namespace Web.Pages
{
    public class ContentModel : PageModel
    {
        public Course Course { get; set; }
        public bool IsEnrolled { get; set; }

        public void OnGet(int courseId)
        {
            // Retrieve the course from the service based on courseId
            var result = new CourseService().GetCourseById(courseId);

            if (result.Success && result.Data is Course course)
            {
                Course = course;
                // Check if the user is enrolled (you can implement user validation based on session or authentication)
                // Assuming you have a service for checking enrollment.
                IsEnrolled = CheckIfUserIsEnrolled(courseId);
            }
            else
            {
                // Handle the case when the course isn't found
                // Redirect to the available courses page or show an error message
                RedirectToPage("/UserCourses");
            }
        }

        private bool CheckIfUserIsEnrolled(int courseId)
        {
            // Replace with actual logic to check if the user is enrolled in the course
            // For example, query the database for the user enrollment status
            return true; // Assuming the user is always enrolled for now
        }
    }
}
