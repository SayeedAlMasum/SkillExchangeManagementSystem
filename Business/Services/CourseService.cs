//CourseService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Context;

namespace Business.Services
{
    public class CourseService
    {
        SkillExchangeContext skillExchangeContext = new SkillExchangeContext();

        public Result GetCourseById(int courseId)
        {
            try
            {
                // Retrieve the course by ID from the database
                var course = skillExchangeContext.Course.FirstOrDefault(c => c.CourseId == courseId);

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
        { //logics
            try
            {
                var courses = skillExchangeContext.Course.ToList();
                return new Result(true, "Success", courses);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}
