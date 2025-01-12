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
        public Result Registration(UserForm user)
        {
            SkillExchangeContext SkillExchangeContext = new SkillExchangeContext();
            bool x = SkillExchangeContext.UserInfo.Any(x => x.Email == user.Email);
            if (x) return new Result(false, "Email already registered!");
            UserInfo userInfo = new UserInfo();
            userInfo.Name = user.FullName;
            userInfo.Email = user.Email;
            userInfo.PasswordHash = new PasswordHasher<object>().HashPassword(user, user.Password);
            userInfo.RoleId = user.RoleId == 0 ? 3 : user.RoleId;
            userInfo.IsActive = true;
            SkillExchangeContext.UserInfo.Add(userInfo);
            try
            {
                SkillExchangeContext.SaveChanges();
                return new Result(true, "Registered Successfully!", user);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public Result Login(string email, string password)
        {
            using var context = new SkillExchangeContext();
            var user = context.UserInfo.FirstOrDefault(u => u.Email == email);
            if (user == null) return new Result(false, "User not found!");

            var passwordHasher = new PasswordHasher<object>();
            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            return verificationResult == PasswordVerificationResult.Success
                ? new Result(true, "Login successful!", user)
                : new Result(false, "Invalid password!");
        }

    }
}