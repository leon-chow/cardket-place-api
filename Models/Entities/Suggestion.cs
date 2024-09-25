using System.ComponentModel.DataAnnotations;

namespace cardket_place_be.Models.Entities
{
  public class Suggestion
  {
    [Required]
    public string CardName;
    public string CategoryName;
    public double price;
    public string? imageUrl;
    public string LastModified;

  }
}