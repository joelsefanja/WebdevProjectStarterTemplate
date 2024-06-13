using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace nhlstendencafe.Pages.Account
{
    public class Index : PageModel
    {
        public string? firstName { get; set; }
        public string? lastName { get; set; }

        public IActionResult OnGet()
        {
            var isAuthenticated = User?.Identity?.IsAuthenticated ?? false;
            
            if (!isAuthenticated) return RedirectToPage("/Index");

            // Retrieve user information
            firstName = User?.FindFirst(ClaimTypes.GivenName)?.Value;
            lastName = User?.FindFirst(ClaimTypes.Surname)?.Value;

            // Ober is logged in
            return Page();
        }
    }
}