//AdminLogIn.cshtml.cs
using Business.FormModel;
using Business.Services;
using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class AdminLogInModel : PageModel
    {

        [BindProperty]
        public UserLogInForm AdminForm { get; set; }

        private readonly UserInfoService _userInfoService;

        // Constructor with Dependency Injection
        public AdminLogInModel(UserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if loginForm is null
            if (AdminForm == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }

            // Attempt login
            Result result = _userInfoService.LogIn(AdminForm);

            if (result.Success)
            {
                // Redirect to the home page if successful
                return RedirectToPage("/Index");
            }
            else
            {
                // Add error message and reload the page
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return Page();
            }
        }
    }
}