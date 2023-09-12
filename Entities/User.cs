using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orderService.domain.Entities
{
    public class User 
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public ICollection<Order> Orders { get; set; }
      
    }
}
