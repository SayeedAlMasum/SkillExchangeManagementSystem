//ResetPassword.cshtml.sc
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly UserInfoService _userInfoService;

        public ResetPasswordModel(UserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Token { get; set; }

        [BindProperty]
        public string NewPassword { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        public IActionResult OnGet(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Account/ForgotPassword");
            }

            Email = email;
            Token = token;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (NewPassword != ConfirmPassword)
            {
                ModelState.AddModelError("", "Passwords don't match.");
                return Page();
            }

            var result = _userInfoService.ResetPassword(Email, Token, NewPassword);
            if (result.Success)
            {
                return RedirectToPage("/Account/ResetPasswordConfirmation");
            }

            ModelState.AddModelError("", result.Message);
            return Page();
        }
    }
}