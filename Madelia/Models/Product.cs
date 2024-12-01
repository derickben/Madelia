using System.ComponentModel.DataAnnotations;

namespace Madelia.Models
{
    public class Product
    {
        [Key]
        [Required(ErrorMessage = "Please enter a product ID!")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please enter a product name!")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Please enter a Product Description!")]
        public string Description { get; set; }

        public string? ProductImage { get; set; }

        [Required(ErrorMessage = "Please enter a price!")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter quantity!")]
        public int StockQuantity { get; set; }

        // Navigation property for the many-to-many relationship
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
