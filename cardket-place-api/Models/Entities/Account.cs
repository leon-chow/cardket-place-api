using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace cardket_place_api.Models.Entities
{
    public class Account : IdentityUser
    {
        public string? Nickname { get; set; }
        public string? ImageUrl { get; set; }

    }
}
