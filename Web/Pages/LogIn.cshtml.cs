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

          
            if (Email == "admin@example.com" && Password == "password123")
            {
                // If login is successful, redirect to a protected page (e.g., Dashboard)
                return RedirectToPage("/Dashboard");
            }

            // If login fails, add a model error and redisplay the login page
            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return Page();
        }
    }
}
