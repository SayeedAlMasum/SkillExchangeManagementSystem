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
