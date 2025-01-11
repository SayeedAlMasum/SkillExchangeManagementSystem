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
                return Page(); // Return the same page with validation errors
            }

            // Example: Replace this with actual login validation logic
            if (Email == "admin@example.com" && Password == "password123")
            {
                // Log in success - Redirect to a protected page (e.g., Dashboard)
                return RedirectToPage("/Dashboard");
            }

            // If credentials are invalid
            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return Page();
        }
    }
}
