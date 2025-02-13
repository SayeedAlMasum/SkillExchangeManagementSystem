//Register.cshtml.cs
using Business;
using Business.FormModel;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public UserRegisterForm userRegisterForm { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Result result = new UserInfoService().Registration(userRegisterForm);
            if (result.Success)
                return RedirectToPage("/LogIn");
            else return Page();
        }
    }
}
