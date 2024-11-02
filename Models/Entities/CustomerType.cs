using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.Models.Entities
{
    public class CustomerType
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string Customtype { get; set; }
        [Required]
        public string CustomDescription { get; set; }
    }
}
