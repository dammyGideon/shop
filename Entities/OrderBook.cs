using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orderService.domain.Entities
{
    public class OrderBook
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
