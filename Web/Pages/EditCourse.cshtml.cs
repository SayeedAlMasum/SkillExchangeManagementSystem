// EditCourse.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Business.Services;
using Database.Model;
using System;
using Database.Context;
using System.Linq;

namespace Web.Pages
{
    public class EditCourseModel : PageModel
    {
        [BindProperty]
        public Course Course { get; set; }

        public IActionResult OnGet(int id)
        {
            var result = new CourseService().GetCourseById(id);

            if (result.Success && result.Data is Course course)
            {
                Course = course;
                return Page();
            }

            return RedirectToPage("/Course");
        }

        public IActionResult OnPostUpdate()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var context = new SkillExchangeContext())
                    {
                        var existingCourse = context.Course.FirstOrDefault(c => c.CourseId == Course.CourseId);

                        if (existingCourse == null)
                        {
                            ModelState.AddModelError(string.Empty, "Course not found.");
                            return Page();
                        }

                        existingCourse.Title = Course.Title;
                        existingCourse.Description = Course.Description;
                        existingCourse.Category = Course.Category;
                        existingCourse.SubCategory = Course.SubCategory;
                        existingCourse.IsPremium = Course.IsPremium;
                        existingCourse.UpdatedDate = DateTime.Now;

                        context.SaveChanges();
                    }

                    return RedirectToPage("/Course");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                }
            }

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            return Page();
        }
    }
}
