using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using MySqlX.XDevAPI;
using nhlstendencafe.Models;
using nhlstendencafe.Repositories;
using nhlstendencafe.SessionExtensions;



namespace nhlstendencafe.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginCredentials loginCredentials { get; set; }

        [BindProperty] public Guid sessioId2 { get; set; }
        
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
        

            UserRepository userRepository = new UserRepository();
            User existingUser = userRepository.GetUserByEmail(loginCredentials.Email);

            if (existingUser != null && userRepository.VerifyPassword(existingUser, loginCredentials.Password))
            {
                // Authentication successful
                Console.WriteLine("User authenticated successfully!");
                CreateSessionId();
                


                return RedirectToPage($"/DisplayProducts/{sessioId2}");
            }
            else
            {
                // Authentication failed
                return Page();
            }

            // Form data is valid, perform necessary actions (e.g., login process)

            // Redirect to a success page or perform any other desired actions
            return RedirectToPage("/Index");
            
            
        }
        
        private void CreateSessionId()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            
            // Create a session key for the model
            Guid gui = Guid.NewGuid();
            string sessionId = gui.ToString();

            // Save the SessionId to session
            session.SetObject("SessionId", sessionId);

            sessioId2 = gui;
        }
        

    }
}