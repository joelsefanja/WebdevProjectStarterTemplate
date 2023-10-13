using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using nhlstendencafe.Models;
using nhlstendencafe.Repositories;

namespace nhlstendencafe.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty] 
        public RegisterCredentials registerCredentials { get; set; }
        private readonly ILogger<RegisterModel> _logger;
        private string errorMessage = string.Empty; 
        
        public RegisterModel(ILogger<RegisterModel> logger)
        {
            _logger = logger;
        }
        public void OnGet(){}
        
        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Invalid input
                    // Handle or display validation errors
                    return Page();
                }

                var newUser = new User
                {
                    Email = registerCredentials.Email,
                    Password = registerCredentials.Password,
                };
                
                var userRepository = new UserRepository();
                userRepository.RegisterUser(newUser);

                // Registration successful
                
                return RedirectToPage("/Login");

          
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error occurred while registering user.");
                errorMessage = "Er is een fout opgetreden bij de registratie.";
            }
            return Page();

        }
    }
 }