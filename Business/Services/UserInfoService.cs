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
            if (skillExchangeContext.UserInfo.Any(x => x.Email == user.Email))
                return new Result(false, "Email already registered!");

            var userInfo = new UserInfo
            {
                Name = user.Name,
                Email = user.Email,
                PasswordHash = new PasswordHasher<UserInfo>().HashPassword(null, user.Password),
                RoleId = 3, // Default to Student
                IsActive = true,
                Location = "Unknown"  // Ensure Location is never NULL

            };

            skillExchangeContext.UserInfo.Add(userInfo);
            skillExchangeContext.SaveChanges();

            return new Result(true, "Registered Successfully!");
        }
        public Result LogIn(UserLogInForm user)
        {
            var userInfo = skillExchangeContext.UserInfo.FirstOrDefault(x => x.Email == user.Email);

            if (userInfo == null)
            {
                return new Result(false, "Email not found. Please register first.");
            }
            // Handle NULL values safely
            string location = userInfo.Location ?? "Unknown"; // Assign a default value if NULL

            Console.WriteLine($"User found: Email={userInfo.Email}, PasswordHash={userInfo.PasswordHash}, Location={location}");


            if (string.IsNullOrEmpty(userInfo.PasswordHash))  // Check if the PasswordHash is null or empty
            {
                return new Result(false, "Password hash not found. Please try again.");
            }

            var passwordVerification = new PasswordHasher<UserInfo>().VerifyHashedPassword(userInfo, userInfo.PasswordHash, user.Password);

            if (passwordVerification == PasswordVerificationResult.Success)
            {
                return new Result(true, $"{userInfo.Name} successfully logged in!");
            }
            else
            {
                return new Result(false, "Incorrect password.");
            }
        }





        public Result Update(UserRegisterForm user)
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