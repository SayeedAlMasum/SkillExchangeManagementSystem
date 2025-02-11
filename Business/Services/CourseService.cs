// CourseService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using Database.Context;
using Database.Model;

namespace Business.Services
{
    public class CourseService
    {
        private readonly SkillExchangeContext _skillExchangeContext;

        public CourseService()
        {
            _skillExchangeContext = new SkillExchangeContext();
        }

        public Result GetCourseById(int courseId)
        {
            try
            {
                var course = _skillExchangeContext.Course.FirstOrDefault(c => c.CourseId == courseId);
                if (course == null)
                {
                    return new Result(false, "Course not found");
                }
                return new Result(true, "Success", course);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public Result List()
        {
            try
            {
                var courses = _skillExchangeContext.Course.ToList();
                return new Result(true, "Success", courses);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public Result AddCourse(Course course)
        {
            try
            {
                // Set the CreatedDate of the course to the current date and time
                course.CreatedDate = DateTime.Now;

                // Add the course to the database context
                _skillExchangeContext.Course.Add(course);

                // Save changes to the database
                _skillExchangeContext.SaveChanges();

                // Return a success result with a message
                return new Result(true, "Course added successfully");
            }
            catch (Exception ex)
            {
                // If an exception occurs, return a failure result with the error message
                return new Result(false, ex.Message);
            }
        }


        public Result UpdateCourse(Course updatedCourse)
        {
            try
            {
                var course = _skillExchangeContext.Course.FirstOrDefault(c => c.CourseId == updatedCourse.CourseId);
                if (course == null)
                {
                    return new Result(false, "Course not found");
                }
                course.Title = updatedCourse.Title;
                course.Description = updatedCourse.Description;
                course.Category = updatedCourse.Category;
                course.SubCategory = updatedCourse.SubCategory;
                course.UpdatedDate = DateTime.Now;
                course.UpdatedBy = updatedCourse.UpdatedBy;
                _skillExchangeContext.SaveChanges();
                return new Result(true, "Course updated successfully");
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public Result DeleteCourse(int courseId)
        {
            try
            {
                var course = _skillExchangeContext.Course.FirstOrDefault(c => c.CourseId == courseId);
                if (course == null)
                {
                    return new Result(false, "Course not found");
                }
                _skillExchangeContext.Course.Remove(course);
                _skillExchangeContext.SaveChanges();
                return new Result(true, "Course deleted successfully");
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}
