//UserInfoService.cs
using Business.FormModel;
using Database.Context;
using Database.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Services;
namespace Business.Services
{

        public class UserInfoService
        {
            SkillExchangeContext skillExchangeContext = new SkillExchangeContext();
            public Result Registration(UserRegisterForm user)
            {
                bool x = skillExchangeContext.UserInfo.Any(x => x.Email == user.Email);
                if (x) return new Result(false, "Email already registered!");
                UserInfo userInfo = new UserInfo();
                userInfo.Name = user.Name;
                userInfo.Email = user.Email;
                userInfo.PasswordHash = new PasswordHasher<UserInfo>().HashPassword(userInfo, user.Password);
                userInfo.RoleId = user.RoleId == 0 ? 3 : user.RoleId;
                userInfo.IsActive = true;
                skillExchangeContext.UserInfo.Add(userInfo);
                return new Result().DBCommit(skillExchangeContext, "Registered Successfully!", null, user);
            }



        public Result LogIn(UserLogInForm user)
        {
            UserInfo? userInfo = skillExchangeContext.UserInfo.Where(x => x.Email == user.Email).FirstOrDefault();
            if (userInfo == null) return new Result(false, "Register First!");

            PasswordVerificationResult HashResult = new PasswordHasher<UserInfo>().VerifyHashedPassword(userInfo, userInfo.PasswordHash, user.Password);
            if (HashResult != PasswordVerificationResult.Failed)
            {
                return new Result(true, $"{userInfo.Name} successfully logged in!");
            }
            else
            {
                return new Result(false, "Incorrect Password");
            }
        }
        public Result Update(UserForm user)
        {
            //logics
            return new Result().DBCommit(skillExchangeContext, "Updated Successfully!", null, user);
        }
        public Result List()
        {
            //logics
            try
            {
                var Users = skillExchangeContext.UserInfo.ToList();
                return new Result(true, "Success", Users);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public Result Single(string Id)
        {
            //logics
            try
            {
                var User = skillExchangeContext.UserInfo.Where(x => x.UserInfoId == Id).FirstOrDefault();
                return new Result(true, "Success", User);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}