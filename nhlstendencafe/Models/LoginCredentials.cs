using System.ComponentModel.DataAnnotations;

namespace nhlstendencafe.Models;

public class LoginCredentials
{
    [Required(ErrorMessage = "E-mail is verplicht")]
    [EmailAddress(ErrorMessage = "Ongeldig e-mail adres")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Wachtwoord is verplicht")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}