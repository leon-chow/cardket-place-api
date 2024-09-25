using System.ComponentModel.DataAnnotations;

namespace cardket_place_be.Models.Entities
{
  public class Category
  {
    [Key]
    public required string Id;
    public string CategoryName;
    public string CategoryDescription;
    public string CategoryCode;
    public string? ImageUrl;

  }
}