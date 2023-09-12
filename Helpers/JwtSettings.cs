using Microsoft.IdentityModel.Tokens;
using orderService.domain.Entities;
using System.Text;

namespace ShopCart.Helpers
{
    public class JwtSettings { 
        public string securityKey { get; set; }
    }
}
