using ShopCart.Dtos;

namespace ShopCart.Contacts
{
    public interface IUser
    {
        public Task<bool> RegisterUserAsync(AuthenticationDto registrationDto);
        public Task<Object> Login(AuthenticationDto registrationDto);
    }
}
