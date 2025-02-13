//LogIn.cshtml.cs
using Business;
using Business.FormModel;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class LogInModel : PageModel
    {
        [BindProperty]
        public UserLogInForm userLogInForm { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            Result result = new UserInfoService().LogIn(userLogInForm);
            if (result.Success)
            {
                return RedirectToPage("/Index"); // Change to a page you want after login
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Message); // Show error in UI
                return Page(); // Return to the same page with validation errors
            }
        }

    }
}
