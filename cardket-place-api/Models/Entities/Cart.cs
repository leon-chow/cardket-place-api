using System.ComponentModel.DataAnnotations;

namespace cardket_place_api.Models.Entities
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public Card[] Items { get; set; }
        public double TotalCost { get; set; }
        public string LastModified { get; set; }

    }
}
