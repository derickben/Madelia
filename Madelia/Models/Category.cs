﻿using System.ComponentModel.DataAnnotations;

namespace Madelia.Models
{
    public class Category
    {
        [Key]
        [Required(ErrorMessage = "Please enter a Category ID!")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter a Category Name!")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Please enter a Category Description!")]
        public string Description { get; set; }

        // Navigation property for the many-to-many relationship
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
