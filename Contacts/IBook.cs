using ShopCart.Dtos;

namespace ShopCart.Contacts
{
    public interface IBook
    {
        public Task<IEnumerable<ReadBookDtos>> GetAllBook();
        public Task<ReadBookDtos> GetBook(int id);

        public Task<ApiResponse<string>> OrderBooks(OrderDtos order, int userId);

        public Task<OrderDetailDto> GetOrderDetails(OrderStatusDto orderStatusDto, int userId);
    }
}
