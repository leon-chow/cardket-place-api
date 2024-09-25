using System.ComponentModel.DataAnnotations;

namespace cardket_place_api.Models.Entities
{
    public class Category
    {
        [Key]
        public required string Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryCode { get; set; }
        public string? ImageUrl { get; set; }

    }
}
