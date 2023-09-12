using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using orderService.domain;
using orderService.domain.Entities;
using ShopCart.Contacts;
using ShopCart.Dtos;
using ShopCart.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopCart.Services
{
}
    public class UserService : IUser
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtSettings jwtSettings;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UserService(ApplicationDbContext dbContext,
            IOptions<JwtSettings> options,
            IMapper mapper,
            IConfiguration configuration
            )
        {
            _context = dbContext;
            jwtSettings = options.Value;
            _mapper = mapper;
        _configuration = configuration;
        }


        public async Task<bool> RegisterUserAsync(AuthenticationDto registrationDto)
        {

            var EmailExists = await _context.Users.FirstOrDefaultAsync(u => u.UserName == registrationDto.Email);

            if (EmailExists != null)
            {
                throw new Exception("This user already exists.");
            }
            var newUser = new User
            {
                UserName = registrationDto.Email,
                Password = PasswordHelper.EncryptPassword(registrationDto.Password)
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return true;
        }

    //Authenticatin 
    public async Task<object> Login(AuthenticationDto authenticationDto)
    {
        try
        {   //checked if the dto is empty
            if (string.IsNullOrWhiteSpace(authenticationDto.Email) || string.IsNullOrWhiteSpace(authenticationDto.Password))
            {
                var response = ApiResponseHelper.Failure<string>("Invalid input data");
                return response;
            }
            //check if the email already exist
            var user = await _context.Users.FirstOrDefaultAsync(item => item.UserName == authenticationDto.Email);

            if (user != null)
            {
                var hashedPassword = user.Password;
                //password verification 
                var enterPassword = PasswordHelper.VerifyPassword(authenticationDto.Password, hashedPassword);

                if (enterPassword)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:key"]);

                    string accessToken = CreateUser(user, tokenHandler, tokenKey);

                    var response = ApiResponseHelper.DataResponse(accessToken, "Authentication successful");
                    return response;
                }
                else
                {
                    var response = ApiResponseHelper.Failure<string>("Invalid User Credentials");
                    return response;
                }
            }
            else
            {
                var response = ApiResponseHelper.Failure<string>("Unauthorized user");
                return response;
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions, log them, and return an appropriate error response.
            var response = ApiResponseHelper.Failure<string>("An error occurred during authentication.");
            return response;
        }
    }
    //create usertoken 
    private string CreateUser(User? user, JwtSecurityTokenHandler tokenHandler, byte[] tokenKey)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]{
                               new Claim("UserId", user.Id.ToString()),
                               new Claim("Email", user.UserName.ToString())
                             }),
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JWT:ExpiresInMins"])),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(token);
        return accessToken;
    }
}
