using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.Models.ViewModels
{
    public class CutomerTypeModel
    {
        [Key]
        public  int CustomerId { get; set; }
        public required string Customtype { get; set; }
        public string CustomDescription { get; set; }
    }
}
