using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orderService.domain.Entities
{
   

    public class Order
    {
        [Key]
        public int Id { get; set; }
        public OrderStatus OrderStatus {  get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public List<OrderBook> OrderBooks { get; set; }
    }
   
     public enum OrderStatus
    {
        Processing  = 1,
        Shipped =2,
        Delivered = 3,
        Cancelled =4,
    }
   
  
    
}
