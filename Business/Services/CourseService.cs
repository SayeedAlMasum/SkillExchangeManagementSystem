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

        public Result ListByTeacher(string teacherEmail)
        {
            try
            {
                var courses = _skillExchangeContext.Course
                    .Where(c => c.TeacherEmail == teacherEmail)
                    .ToList();
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
                if (string.IsNullOrEmpty(course.Title))
                    return new Result(false, "Title is required.");
                if (string.IsNullOrEmpty(course.Description))
                    return new Result(false, "Description is required.");
                if (string.IsNullOrEmpty(course.Category))
                    return new Result(false, "Category is required.");
                if (string.IsNullOrEmpty(course.SubCategory))
                    return new Result(false, "SubCategory is required.");

                if (course.CourseId == 0)
                {
                    course.CourseId = _skillExchangeContext.Course.Any()
                        ? _skillExchangeContext.Course.Max(c => c.CourseId) + 1
                        : 1;
                }

                course.CreatedDate = DateTime.Now;

                _skillExchangeContext.Course.Add(course);
                _skillExchangeContext.SaveChanges();

                return new Result(true, "Course added successfully");
            }
            catch (Exception ex)
            {
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
                course.TeacherEmail = updatedCourse.TeacherEmail; // ensure this is updated
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
