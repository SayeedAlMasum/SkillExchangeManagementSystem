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
        public Result Registration(UserRegisterForm user, string role)
        {
            if (skillExchangeContext.UserInfo.Any(x => x.Email == user.Email))
                return new Result(false, "Email already registered!");

            var userInfo = new UserInfo
            {
                Name = user.Name,
                Email = user.Email,
                PasswordHash = new PasswordHasher<UserInfo>().HashPassword(null, user.Password),
                Role = role, // Set the role here
                IsActive = true,
                Location = "Unknown"
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

            var passwordVerification = new PasswordHasher<UserInfo>().VerifyHashedPassword(userInfo, userInfo.PasswordHash, user.Password);

            if (passwordVerification == PasswordVerificationResult.Success)
            {
                // Return the role along with the success message
                return new Result(true, $"{userInfo.Name} successfully logged in!", userInfo.Role);
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
        public Result GeneratePasswordResetToken(string email)
        {
            var user = skillExchangeContext.UserInfo.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return new Result(false, "Email not found.");
            }

            // In a real application, generate a secure token and store it with an expiration date
            var token = Guid.NewGuid().ToString();

            // Here you would typically:
            // 1. Store the token in the database with an expiration date
            // 2. Send an email with a link containing the token

            return new Result(true, "Password reset token generated", token);
        }

        public Result ResetPassword(string email, string token, string newPassword)
        {
            // In a real application, you would:
            // 1. Verify the token is valid and not expired
            // 2. Update the password

            var user = skillExchangeContext.UserInfo.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return new Result(false, "Email not found.");
            }

            user.PasswordHash = new PasswordHasher<UserInfo>().HashPassword(null, newPassword);
            skillExchangeContext.SaveChanges();

            return new Result(true, "Password reset successfully.");
        }
    }
}