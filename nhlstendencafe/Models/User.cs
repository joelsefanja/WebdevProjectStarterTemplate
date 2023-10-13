using System.ComponentModel.DataAnnotations;

namespace nhlstendencafe.Models
{
    public class User
    {
        public int Id { get; set; }
    
        [Required(ErrorMessage = "E-mail is verplicht")]
        [EmailAddress(ErrorMessage = "Ongeldig e-mail adres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    
        [Required(ErrorMessage = "Wachtwoord herhaling is verplicht")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Wachtwoorden komen niet overeen")]
        public string ConfirmPassword { get; set; }

        public string PasswordHash { get; set; }
    }
}