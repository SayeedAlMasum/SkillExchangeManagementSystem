//LogIn.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
 
    public class LogInModel : PageModel
    {
 
        [BindProperty]
        public string Email { get; set; }


        [BindProperty]
        public string Password { get; set; }


        public IActionResult OnGet()
        {
            return Page(); 
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Bypass authentication checks and redirect
            return RedirectToPage("/Dashboard");
        }

    }
}
