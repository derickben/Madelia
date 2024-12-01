using System.ComponentModel.DataAnnotations;

namespace Madelia.Models
{
    public class Order
    {
        [Key]
        [Required(ErrorMessage = "Please enter an Order ID!")]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Please provide a staus for the order!")]
        public string Status { get; set; } // e.g., "Pending", "Shipped", "Completed"

        [Required(ErrorMessage = "Please enter a Customer ID!")]
        public int CustomerId { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
