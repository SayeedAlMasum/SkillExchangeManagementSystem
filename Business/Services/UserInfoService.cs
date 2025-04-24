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
using Microsoft.EntityFrameworkCore;
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

            try
            {
                // Set timeout to 120 seconds (2 minutes)
                skillExchangeContext.Database.SetCommandTimeout(120);
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
            catch (Exception ex)
            {
                return new Result(false, "Login operation timed out. Please try again.");
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
            try
            {
                var user = skillExchangeContext.UserInfo
                    .FirstOrDefault(x => x.Email == email);

                if (user == null)
                {
                    // Return success even if user not found to prevent email enumeration
                    return new Result(true, "If your email exists in our system, you'll receive a password reset link.");
                }

                // Generate a secure token
                var token = Guid.NewGuid().ToString();

                // Set token and expiration (e.g., 1 hour)
                user.PasswordResetToken = token;
                user.ResetTokenExpires = DateTime.UtcNow.AddHours(1);
                skillExchangeContext.SaveChanges();

                // In production, you would send an email here with the reset link
                // For now, we'll just return the token for testing
                return new Result(true, "Password reset token generated", token);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public Result ResetPassword(string email, string token, string newPassword)
        {
            using (var transaction = skillExchangeContext.Database.BeginTransaction())
            {
                try
                {
                    var user = skillExchangeContext.UserInfo
                        .FirstOrDefault(x => x.Email == email &&
                                           x.PasswordResetToken == token &&
                                           x.ResetTokenExpires > DateTime.UtcNow);

                    if (user == null)
                    {
                        return new Result(false, "Invalid or expired password reset token.");
                    }

                    user.PasswordHash = new PasswordHasher<UserInfo>().HashPassword(null, newPassword);
                    user.PasswordResetToken = null;
                    user.ResetTokenExpires = null;

                    skillExchangeContext.SaveChanges();
                    transaction.Commit();

                    return new Result(true, "Password reset successfully.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Result(false, ex.Message);
                }
            }
        }
    }
}