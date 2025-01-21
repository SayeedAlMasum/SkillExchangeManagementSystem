using Business.FormModel;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages
{
    // Represents the Razor Page Model for user registration
    public class RegisterModel : PageModel
    {
        // Binds the Full Name input field from the form to this property
        // Validation: Full Name is required
        [BindProperty]
        [Required(ErrorMessage = "Full Name is required.")]
        public string Name { get; set; }

        // Binds the Email input field from the form to this property
        // Validation: Email is required and must follow a valid email format
        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        // Binds the Password input field from the form to this property
        // Validation: Password is required and displayed as a password input in the form
        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Handles the POST request when the user submits the registration form
        public IActionResult OnPostRegister()
        {
            if (!ModelState.IsValid)
            {
                // Return the page with validation errors
                return Page();
            }

            // Create a UserForm object with the registration details
            var userForm = new UserForm
            {
                Name = Name,
                Email = Email,
                Password = Password,
                IsActive = true,
                RoleId = 0 // Default role; can be set to a specific value if needed
            };

            // Create an instance of the UserInfoService
            var userInfoService = new UserInfoService();

            // Call the Registration method and capture the result
            var result = userInfoService.Registration(userForm);

            // Check the result of the registration
            if (result.Success)
            {
                // Redirect to the login page on successful registration
                return RedirectToPage("/LogIn");
            }
            else
            {
                // Add a model error for registration failure
                ModelState.AddModelError(string.Empty, result.Message);
                return Page();
            }
        }


    }
}
        