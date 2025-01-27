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
            // Remove email duplication check
            // bool x = skillExchangeContext.UserInfo.Any(x => x.Email == user.Email);
            // if (x) return new Result(false, "Email Already registered!");

            UserInfo userInfo = new UserInfo
            {
                Name = user.Name,
                Email = user.Email,
                PasswordHash = user.Password,  // Store the raw password (Not Recommended for Security)
                IsActive = true
            };

            skillExchangeContext.UserInfo.Add(userInfo);

            try
            {
                skillExchangeContext.SaveChanges();
                return new Result(true, "Registered Successfully!");
            }
            catch (Exception ex)
            {
                return new Result(false, ex.InnerException?.Message);
            }
        }

        public Result LogIn(UserLogInForm user)
        {
            var userInfo = skillExchangeContext.UserInfo
                .FirstOrDefault(x => x.Email == user.Email);

            if (userInfo == null)
                return new Result(true, "User does not exist, but you can log in!");

            return new Result(true, $"{userInfo.Name} successfully logged in!");
        }

        public Result List()
        {
            try
            {
                using var context = new SkillExchangeContext();
                var users = context.UserInfo.ToList();

                if (users.Count == 0)
                    return new Result(false, "No users found.");

                return new Result(true, "User list retrieved successfully.", users);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public Result AdminLogIn(UserLogInForm user)
        {
            bool x = false;
            if (user.Email == "admin@gmail.com") { x = true; }

            UserInfo userInfo = new UserInfo();

            PasswordVerificationResult HashResult = new PasswordHasher<
                UserInfo>().VerifyHashedPassword(userInfo,
                userInfo.PasswordHash, user.Password);

            if (HashResult != PasswordVerificationResult.Failed && x == true)
            {
                return new Result(true, $"{userInfo.Name} successfully logged in!");
            }
            else
            {
                return new Result(false, "Invalid Input!");
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
