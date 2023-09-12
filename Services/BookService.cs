using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using orderService.domain;
using orderService.domain.Entities;
using ShopCart.Contacts;
using ShopCart.Dtos;
using ShopCart.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShopCart.Services
{
    public class BookService : IBook
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
   
       
        public BookService(ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            
            
        }
        //This method out get all the available books from the database
        public async Task<IEnumerable<ReadBookDtos>> GetAllBook()
        {
            var books = await _context.Books.ToListAsync();
            if (books == null || !books.Any()) throw new ArgumentNullException("There is not book in the store");

            var booksdto = _mapper.Map<IEnumerable<ReadBookDtos>>(books);
            return booksdto;
        }
        // This method get single book from the database
        public async Task<ReadBookDtos> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) throw new ArgumentNullException($"The book with this {id} does not exist in the store");
            var dtos = _mapper.Map<ReadBookDtos>(book);
            return dtos;
        }
        //This method create order based on the authenticated user
        public async Task<ApiResponse<string>> OrderBooks(OrderDtos order, int userId)
        {
            // starts a new database transaction
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //check if the user is authenticated
                    if (userId != null)
                    {
                        //check if the product is available
                        var productId = await _context.Books.FirstOrDefaultAsync(p => p.Id == order.ProductId);
                        if (productId == null)
                        {
                            transaction.Rollback(); 
                            var response = ApiResponseHelper.Failure<string>($"This product is not available");
                            return response;
                        }

                        var productOrder = new Order
                        {
                            OrderStatus = OrderStatus.Processing,
                            UserId = userId
                        };

                        _context.Add(productOrder);
                        await _context.SaveChangesAsync();
                        var orderId = productOrder.Id;

                        var orderbook = new OrderBook
                        {
                            OrderId = orderId,
                            BookId = order.ProductId
                        };
                        _context.Add(orderbook);
                        await _context.SaveChangesAsync();
                        //rabbitmq publishing message with the productId and the OrderId
                       MessageQueue.PublishMessage(order.ProductId,orderId);
                        transaction.Commit();

                        var result = ApiResponseHelper.Success<string>("Order created Successfully");
                        return result;
                    }
                    else
                    {
                        transaction.Rollback(); 
                        var response = ApiResponseHelper.Failure<string>("This User does not exist");
                        return response;
                    }
                }
                catch (DbUpdateException ex)
                {
                    // Log or handle the DbUpdateException
                    transaction.Rollback(); // Rollback before returning the response
                    return ApiResponseHelper.Failure<string>("A database error occurred while making the order.");
                }
                catch (Exception ex)
                {
                    // Log or handle other exceptions
                    transaction.Rollback(); // Rollback before returning the response
                    return ApiResponseHelper.Failure<string>("An error occurred while making the order.");
                }
            }
        }

        //this method get the details of the order based on the authenticated user
        public async Task<OrderDetailDto> GetOrderDetails(OrderStatusDto orderStatusDto, int userId)
        {
            
            var order = await _context.Orders
                .Include(o => o.OrderBooks)
                .ThenInclude(ob => ob.Book)
                .FirstOrDefaultAsync(o => o.Id == orderStatusDto.OrderId && o.UserId == userId);

            if (order == null)
            {
                // Handle the case where the order with the specified ID is not found.
                var response = ApiResponseHelper.Failure<string>("Order not found.");
                return null;
            }

            var orderDetailsDto = new OrderDetailDto
            {
                OrderId = order.Id,
                OrderStatus = order.OrderStatus != null ? Enum.GetName(typeof(OrderStatus), order.OrderStatus) : null, // Convert enum value to its name with null check
                Books = order.OrderBooks.Select(ob => new BookDto
                {

                    Id = ob.Book.Id,
                    Title = ob.Book.Title,
                    Description = ob.Book.Description,
                    IsbnNumber = ob.Book.IsbnNumber,
                    Price = ob.Book.Price
                }).ToList()
            };
          
            return orderDetailsDto;
        }

      

    }

}
