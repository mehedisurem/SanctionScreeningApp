using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SanctionScreeningApp.Pages.Html
{
    public class DashboardModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnGetLogout()
        {
            // Implement any necessary logic to clear or sign out the user session
            // For example, you can use the following line to sign out the user:
            // HttpContext.SignOutAsync();

            // Redirect to the login page or any other page after logout
            return RedirectToPage("/Login");
        }
    }
}
