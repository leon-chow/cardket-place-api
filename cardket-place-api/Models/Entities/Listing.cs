using System.ComponentModel.DataAnnotations;

namespace cardket_place_api.Models.Entities
{
    public class Listing
    {
        [Key]
        public required string Id { get; set; }
        public string ListingName { get; set; }
        public string Category { get; set; }
        public string CategoryCode {  get; set; }
        public string Condition {  get; set; }
        public double Price {  get; set; }
        public int Quantity {  get; set; }
        public string? ImageUrl {  get; set; }

    }
}
