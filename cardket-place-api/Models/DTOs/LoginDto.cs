using System.ComponentModel.DataAnnotations;

namespace cardket_place_api.Models.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
