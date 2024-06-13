using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using nhlstendencafe.Models;
using nhlstendencafe.Repositories;
using nhlstendencafe.SessionExtensions;



namespace nhlstendencafe.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginCredentials LoginCredentials { get; set; }
        private UserRepository _userRepository;

        public LoginModel(IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = new UserRepository();
            LoginCredentials = new LoginCredentials();
        }

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            Boolean passwordIsVerified = _userRepository.VerifyPassword(LoginCredentials);

            if (passwordIsVerified)
            {
                // Retrieve user information
                (string firstName, string lastName) userName =
                    _userRepository.GetUserNameByEmail(LoginCredentials.Email);

                // Create claims for user
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.GivenName, userName.firstName),
                    new Claim(ClaimTypes.Surname, userName.lastName)
                };

                // create identity for the user
                var identity = new ClaimsIdentity(claims, "OberAuthentication");

                // create Claims Principal 
                var claimsPrincipal = new ClaimsPrincipal(identity);

                // Sign in the user
                await HttpContext.SignInAsync("OberAuthentication", claimsPrincipal);

                return RedirectToPage("/Account/Index");
            }

            return Page();
        }
    }
}