//Register.cshtml.cs
using Business;
using Business.FormModel;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages
{
    public class RegisterModel : PageModel
    {

        [BindProperty]
        public UserRegisterForm userRegisterForm { get; set; }
        private readonly UserInfoService _userInfoService;

        public RegisterModel(UserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }
        public void OnGet()
        {

        }

        // Handles the POST request when the user submits the registration form
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (userRegisterForm == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid registration attempt");
                return Page();
            }

            // Bypass service call and allow any registration
            return RedirectToPage("/LogIn");
        }

    }

}
        