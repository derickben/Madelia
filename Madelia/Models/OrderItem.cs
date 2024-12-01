using System.ComponentModel.DataAnnotations;

namespace Madelia.Models
{
    public class OrderItem
    {
        [Key]
        [Required(ErrorMessage = "Please enter an OrderItem ID!")]
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } // Price per unit

        // Foreign Keys
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        // Navigation properties
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
