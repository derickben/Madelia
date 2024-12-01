﻿namespace Madelia.Models
{
    public class ProductCategory
    {
        // Foreign Keys
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        // Navigation properties
        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}
