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
        // Initialize the database context to interact with the database
        SkillExchangeContext skillExchangeContext = new SkillExchangeContext();

        // Method to handle the registration of a new user
        public Result Registration(UserRegisterForm user)
        {
            // Check if a user already exists with the given email
            bool x = skillExchangeContext.UserInfo.Any(x => x.Email == user.Email);

            // If the email is already registered, return a failure result with an error message
            if (x) return new Result(false, "Email already registered!");

            // Create a new UserInfo object to store user data
            UserInfo userInfo = new UserInfo();

            // Assign the name, email, and other details to the new UserInfo object
            userInfo.Name = user.Name;
            userInfo.Email = user.Email;

            // Hash the password before saving it to the database
            userInfo.PasswordHash = new PasswordHasher<UserInfo>().HashPassword(userInfo, user.Password);

            // If the RoleId is 0, default it to 3 (Student), otherwise use the provided RoleId
            userInfo.RoleId = user.RoleId == 0 ? 3 : user.RoleId;

            // Set the user's status to active (true)
            userInfo.IsActive = true;

            // Add the new UserInfo object to the UserInfo table in the database
            skillExchangeContext.UserInfo.Add(userInfo);

            // Commit the changes to the database and return a success result with a success message
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