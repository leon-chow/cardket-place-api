using System.ComponentModel.DataAnnotations;

namespace cardket_place_api.Models.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public required string Email {  get; set; }
        
        [Required]
        [MinLength(6)]
        public required string Password { get; set; }    
    }
}
