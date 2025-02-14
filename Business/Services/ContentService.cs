//ContentService.cs
using Database.Context;
using Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Services
{
    public class ContentService
    {
        private readonly SkillExchangeContext _context;

        public ContentService()
        {
            _context = new SkillExchangeContext();
        }

        public Result AddContent(Content content)
        {
            try
            {
                _context.Content.Add(content);
                _context.SaveChanges();
                return new Result(true, "Content added successfully.");
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public Result GetContentByCourseId(int courseId)
        {
            try
            {
                var content = _context.Content.Where(c => c.CourseId == courseId).ToList();
                return new Result(true, "Content retrieved successfully.", content);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}