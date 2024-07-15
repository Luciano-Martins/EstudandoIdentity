using System.ComponentModel.DataAnnotations;

namespace MvcWebIdentity.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]  
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [DataType(DataType.Password)]

        [Display(Name = "Confirme a Senha ")]
        [Compare("Password",ErrorMessage = " Senha não Conferem")]
        public string? ConfirmedPassword { get; set; }

    }
}
