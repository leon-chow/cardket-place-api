using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace cardket_place_be.Models.Entities
{
  public class Account : IdentityUser
  {
    [Key]
    public required string Id;
    [Required]
    public string UserName;
    public string Password;
    public string Nickname;
    public string? ImageUrl;

  }
}