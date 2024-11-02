using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tabaarak.Models.Entities
{
    public class TicketBooking
    {
        [Key]
        public int BookingID { get; set; }

        // Foreign key to Customer
        public int CustomerID { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Today;
        public DateTime VisitDate { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalAmount { get; set; }

        public string PaymentMethod { get; set; }
        public bool DiscountApplied { get; set; }=false;
        [Required]
        public string TicketName { get; set; }
        //public string BookingStatus { get; set; }

        // Navigation property
        public Customer Customer { get; set; }
    }
}
