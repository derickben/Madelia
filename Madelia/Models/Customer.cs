using System.ComponentModel.DataAnnotations;

namespace Madelia.Models
{
    public class Customer
    {
        [Key]
        [Required(ErrorMessage = "Please enter a Customer ID!")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Please enter a First name!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter an email address!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a phone number!")]
        public string PhoneNumber { get; set; }

        // Navigation property for Orders
        public ICollection<Order> Orders { get; set; }
    }
}
