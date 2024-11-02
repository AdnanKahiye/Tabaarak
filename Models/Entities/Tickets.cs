using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.Models.Entities
{
    public class Tickets
    {
        [Key]
        public int TicketId { get; set; }
        [Required]
        [Display(Name = "Ticket Name")]
        public string TicketName { get; set; }

        [Required]
        [Display(Name ="Ticket Discription")]
        public string TicketDescription { get; set;}

        [Required]
        [Display(Name = "Ticket Amount")]
        public Double TicketAmount { get; set; }


        
    }
}
