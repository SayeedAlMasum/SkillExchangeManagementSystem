//RoleService.cs
using Database.Context;
using Database.Model;

namespace Business.Services
{     
    public class RoleService
    {
        SkillExchangeContext skillExchangeContext = new SkillExchangeContext();
        public Result Add(Role role)
        {
            if (skillExchangeContext.Role.Any(x => x.Name == role.Name))
                return new Result(false, "Role already exist!");
            skillExchangeContext.Role.Add(role);
            return new Result().DBCommit(skillExchangeContext, "Save Successfully!", null, role);
        }   
        public Result Update(Role role)
        {
            if (!skillExchangeContext.Role.Any(x => x.RoleId == role.RoleId))
                return new Result(false, "Role not exist!");
            skillExchangeContext.Role.Update(role);
            return new Result().DBCommit(skillExchangeContext, "Updated Successfully!", null, role);
        }
        public Result List()
        {
            try
            {
                var roles = skillExchangeContext.Role.ToList();
                return new Result(true, "Success", roles);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public Result Single(int Id)
        {
            try
            {
                var role = skillExchangeContext.Role.Where(x => x.RoleId == Id).FirstOrDefault();
                return new Result(true, "Success", role);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}