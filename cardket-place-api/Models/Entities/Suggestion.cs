using System.ComponentModel.DataAnnotations;

namespace cardket_place_api.Models.Entities
{
    public class Suggestion
    {
        [Key]
        public string Id { get; set; }  
        public string CardName {  get; set; }
        public string CategoryName {  get; set; }
        public double Price {  get; set; }
        public string? ImageUrl {  get; set; }
        public string LastModified {  get; set; }

    }
}
