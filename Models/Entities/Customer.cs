using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tabaarak.Models.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string CustomerType { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Today;

        public List<TicketBooking> TicketBookings { get; set; }
    }
}
