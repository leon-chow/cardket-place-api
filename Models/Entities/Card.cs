using System.ComponentModel.DataAnnotations;

namespace cardket_place_be.Models.Entities
{
  public class Card
  {
    [Key]
    public required string Id;
    public string Name;
    public string Category;
    public double Price;
    public string ReleaseDate;
    public string CardCode;
    public string ImageUrl;
    public int Quantity;
    public string ProductDetails;
    public object MarketPriceHistory;
    public object Snapshot;
    public string? RedirectUrl;

  }
}