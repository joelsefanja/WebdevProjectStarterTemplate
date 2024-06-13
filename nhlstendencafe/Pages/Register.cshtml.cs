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
        public User user { get; set; }
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
                    return Page();
                }

                var userRepository = new UserRepository();
                userRepository.RegisterUser(user);

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