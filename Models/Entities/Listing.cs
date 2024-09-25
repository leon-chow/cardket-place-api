using System.ComponentModel.DataAnnotations;

namespace cardket_place_be.Models.Entities
{
  public class Listing
  {
    [Key]
    public required string Id;
    public string ListingName;
    public string Category;
    public string CategoryCode;
    public string Condition;
    public double Price;
    public int Quantity;
    public string? ImageUrl;

  }
}