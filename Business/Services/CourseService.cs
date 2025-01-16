//CourseService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Context;
using Database.Model;

namespace Business.Services
{
    public class CourseService
    {
        // Initialize SkillExchangeContext for database operations
        SkillExchangeContext skillExchangeContext = new SkillExchangeContext();

        // Method to retrieve course details by ID
        public Result GetCourseById(int courseId)
        {
            try
            {
                // Retrieve the course by ID from the database
                var course = skillExchangeContext.Course.FirstOrDefault(c => c.CourseId == courseId);
                // Check if course exists
                if (course == null)
                {
                    // Return failure result if course not found
                    return new Result(false, "Course not found");
                }
                // Return success result with course details
                return new Result(true, "Success", course);
            }
            catch (Exception ex)
            {
                // Return failure result in case of exception
                return new Result(false, ex.Message);
            }
        }
        // Method to list all courses
        public Result List()
        { //logics
            try
            {
                // Retrieve all courses from the database
                var courses = skillExchangeContext.Course.ToList();
                // Return success result with course list
                return new Result(true, "Success", courses);
            }
            catch (Exception ex)
            {
                // Return failure result in case of exception
                return new Result(false, ex.Message);
            }
        }
        public Result UpdateCourse(Course updatedCourse)
        {
            try
            {
                // Retrieve the course by ID from the database
                var course = skillExchangeContext.Course.FirstOrDefault(c => c.CourseId == updatedCourse.CourseId);
                // Check if course exists
                if (course == null)
                {
                    // Return failure result if course not found
                    return new Result(false, "Course not found");
                }
                // Update course properties with new values
                course.Title = updatedCourse.Title;
                course.Description = updatedCourse.Description;
                course.Category = updatedCourse.Category;
                course.SubCategory = updatedCourse.SubCategory;
                course.UpdatedDate = DateTime.Now;
                course.UpdatedBy = updatedCourse.UpdatedBy;

                // Save changes to the database
                skillExchangeContext.SaveChanges();

                // Return success result after update
                return new Result(true, "Course updated successfully");
            }
            catch (Exception ex)
            {
                // Return failure result in case of exception
                return new Result(false, ex.Message);
            }
        }

        // Method to delete a course by ID
        public Result DeleteCourse(int courseId)
        {
            try
            {
                // Retrieve the course by ID from the database
                var course = skillExchangeContext.Course.FirstOrDefault(c => c.CourseId == courseId);

                // Check if course exists
                if (course == null)
                {
                    // Return failure result if course not found
                    return new Result(false, "Course not found");
                }

                // Remove the course from the database
                skillExchangeContext.Course.Remove(course);

                // Save changes to the database
                skillExchangeContext.SaveChanges();

                // Return success result after deletion
                return new Result(true, "Course deleted successfully");
            }
            catch (Exception ex)
            {
                // Return failure result in case of exception
                return new Result(false, ex.Message);
            }
        }
    }
}
