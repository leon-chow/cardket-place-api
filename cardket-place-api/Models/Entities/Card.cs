using System.ComponentModel.DataAnnotations;

namespace cardket_place_api.Models.Entities
{
    public class Card
    {
        [Key]
        public required string Id { get; set; } 
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public string ReleaseDate { get; set; }
        public string CardCode { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public string ProductDetails { get; set; }
        public string? RedirectUrl { get; set; }

    }
}
