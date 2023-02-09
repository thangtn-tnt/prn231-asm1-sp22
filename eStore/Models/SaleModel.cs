using System.ComponentModel.DataAnnotations;

namespace eStore.Models
{
    public class SaleModel
    {
        [Required]
        public string StartDate { get; set; }
        [Required]
        public string EndDate { get; set; }
    }
}
