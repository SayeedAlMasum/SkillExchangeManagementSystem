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
            return RedirectToPage("/Account/ForgotPasswordConfirmation");
        }

        }


    }