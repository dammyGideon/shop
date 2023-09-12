using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ShopCart.Contacts;
using ShopCart.Dtos;
using ShopCart.Services;
using System;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace ShopCart.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public readonly IBook _books;
      
        public BooksController(IBook book) {
            _books = book ?? throw new ArgumentNullException(nameof(book));
           
        }
        [AllowAnonymous]
        // Fetching the list of books
        [HttpGet]
        public async Task<IActionResult> GetAllBookAsync()
        {
            var payloads = await _books.GetAllBook();
            return Ok(payloads);
        }
        [AllowAnonymous]
        // Fetching the details of a specific book. 
        [HttpGet("{id}", Name ="product-Details")] 
        public async Task<IActionResult> GetSingleId(int id)
        {
            var payloads = await _books.GetBook(id);
            return Ok(payloads);
        }

        //create order controller
        [HttpPost("order")]
        public async Task<IActionResult> OrderService(OrderDtos orderDtos)
        {
            //get the userid from the claims to perform transaction
            var userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
            
            var order_book = await _books.OrderBooks(orderDtos,userId);
            return Ok(order_book);
        }

        // this controller checks order status
        [HttpPost("order-status")]
        public async Task<IActionResult> OrderStatus(OrderStatusDto orderStatusDto)
        {
            var userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);

            var order_book = await _books.GetOrderDetails (orderStatusDto, userId);
            return Ok(order_book);
        }

    }
}
