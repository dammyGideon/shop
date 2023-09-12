using System.ComponentModel.DataAnnotations;

namespace ShopCart.Dtos
{
    public class AuthenticationDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
