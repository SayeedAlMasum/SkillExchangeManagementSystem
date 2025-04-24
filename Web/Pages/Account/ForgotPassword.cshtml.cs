//ForgotPassword.cshtml.cs
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserInfoService _userInfoService;

        public ForgotPasswordModel(UserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        [BindProperty]
        public string Email { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = _userInfoService.GeneratePasswordResetToken(Email);
            if (result.Success)
            {
                // In a real application, you would send an email with the reset link here
                // For this example, we'll redirect to a confirmation page with the token
                // In production, you should never expose the token in the URL
                return RedirectToPage("/Account/ForgotPasswordConfirmation", new { email = Email, token = result.Data });
            }

            ModelState.AddModelError("", result.Message);
            return Page();
        }
    }
}