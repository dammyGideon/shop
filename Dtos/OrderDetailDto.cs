using orderService.domain.Entities;

namespace ShopCart.Dtos
{
    public class OrderDetailDto
    {
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }

        public List<BookDto> Books { get; set; }
    }
}
