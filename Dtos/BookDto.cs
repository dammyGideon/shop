namespace ShopCart.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IsbnNumber { get; set; }
        public decimal Price { get; set; }
    }
}