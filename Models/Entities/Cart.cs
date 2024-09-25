using System.ComponentModel.DataAnnotations;

namespace cardket_place_be.Models.Entities
{
  public class Cart
  {
    [Key]
    public Card[] Items;
    public double TotalCost;
    public string LastModified;

  }
}