using System.ComponentModel.DataAnnotations;

namespace ShopCart.Dtos
{
    public class ReadBookDtos
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IsbnNumber { get; set; }
        public decimal Price { get; set; }
    }
}
