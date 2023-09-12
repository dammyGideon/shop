
using Microsoft.AspNetCore.Mvc;
using ShopCart.Contacts;
using ShopCart.Dtos;
using ShopCart.Helpers;

namespace ShopCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser _userService;
        public UsersController(IUser userService) {
            _userService= userService ?? throw new ArgumentNullException(nameof(userService));
        }
        // GET: api/<UsersController>
        [HttpPost("register-user")]

        public async Task<IActionResult> CreateUser(AuthenticationDto registrationDto)
        {
            if (!ModelState.IsValid) throw new ArgumentNullException("Input can not  be empty");
            await _userService.RegisterUserAsync(registrationDto);
            var response = ApiResponseHelper.Success<string>("User registration successful");
            return Ok(response);
        }

        [HttpPost("login-user")]
        public async Task<IActionResult> LoginUser([FromBody] AuthenticationDto registrationDto)
        {
                return Ok( await _userService.Login(registrationDto));  
        }

    }
}
