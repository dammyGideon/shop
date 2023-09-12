using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orderService.domain.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Book title is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Book Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Isbn Number is required")]
        public string IsbnNumber { get; set; }
        [Required(ErrorMessage = "Book price is required")]
        public decimal Price { get; set; }

        // Navigation property for the junction table
        public List<OrderBook> OrderBooks { get; set; }
    }
}
