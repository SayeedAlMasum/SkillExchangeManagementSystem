//BusinessTest/Program.cs
using Business;
using Business.FormModel;
using Business.Services;
using Database.Model;
using Microsoft.AspNetCore.Identity;

namespace BusinessTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Registration();
            //LogIn();
            //UserListTest();
            //UserInfoTest();
        }

        static void Registration()
        {
            UserRegisterForm userRegisterForm = new UserRegisterForm();
            Console.WriteLine("Enter Name:");
            userRegisterForm.Name = Console.ReadLine();  // Fixed property from FullName to Name
            Console.WriteLine("Enter Email:");
            userRegisterForm.Email = Console.ReadLine();
            Console.WriteLine("Enter Password:");
            userRegisterForm.Password = Console.ReadLine();
            Result result = new UserInfoService().Registration(userRegisterForm);  // Fixed method name
            Console.WriteLine(result.Message);
        }

        static void LogIn()
        {
            UserLogInForm userLoginForm = new UserLogInForm();
            Console.WriteLine("Enter the Email:");
            userLoginForm.Email = Console.ReadLine();
            Console.WriteLine("Enter the Password:");
            userLoginForm.Password = Console.ReadLine();
            Result result = new UserInfoService().LogIn(userLoginForm);  // Fixed method name
            Console.WriteLine(result.Message);
        }

        static void UserListTest()
        {
            Result result = new UserInfoService().List();
            Console.WriteLine(result.Message);
            if (result.Success)
            {
                // If result is successful, print user details
                foreach (var user in (List<UserInfo>)result.Data)
                {
                    Console.WriteLine($"Name: {user.Name}, Email: {user.Email}, RoleId: {user.RoleId}");
                }
            }
        }

        static void UserInfoTest()
        {
            Console.WriteLine("Enter UserId:");
            string userInfoId = Console.ReadLine();
            Result result = new UserInfoService().Single(userInfoId);
            Console.WriteLine(result.Message);
        }
    }
}
