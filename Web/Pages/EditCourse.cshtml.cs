//EditCourse.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Business.Services;
using Database.Model;
using System;
using Database.Context;

namespace Web.Pages
{
    public class EditCourseModel : PageModel
    {
        [BindProperty]
        public Course Course { get; set; }

        public IActionResult OnGet(int id)
        {
            // Fetch the course by ID
            var result = new CourseService().GetCourseById(id);

            if (result.Success && result.Data is Course course)
            {
                Course = course;
                return Page();
            }

            // If course not found, redirect to course list
            return RedirectToPage("/Course");
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var context = new SkillExchangeContext())
                    {
                        // Find the existing course
                        var existingCourse = context.Course.Find(Course.CourseId);

                        if (existingCourse == null)
                        {
                            ModelState.AddModelError(string.Empty, "Course not found.");
                            return Page();
                        }

                        // Update course properties
                        existingCourse.Title = Course.Title;
                        existingCourse.Description = Course.Description;
                        existingCourse.Category = Course.Category;
                        existingCourse.SubCategory = Course.SubCategory;
                        existingCourse.IsPremium = Course.IsPremium;
                        existingCourse.UpdatedDate = DateTime.Now;

                        // Save changes
                        context.SaveChanges();
                    }

                    return RedirectToPage("/Course");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                }
            }

            // If ModelState is invalid or an error occurs, reload the page with errors
            return Page();
        }
    }
}
