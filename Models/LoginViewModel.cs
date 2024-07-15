using System.ComponentModel.DataAnnotations;

namespace MvcWebIdentity.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "o email é Obrigatório")]
        [EmailAddress(ErrorMessage = "Email Inválido")]
        public string? Email { get; set;}
        [Required(ErrorMessage = "A senha é Obrigatória")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Display(Name = "Lembra-me")]
        public bool RememberMe { get; set; }
    }
}
