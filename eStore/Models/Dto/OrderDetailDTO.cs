using System.ComponentModel.DataAnnotations;

namespace eStore.Models.Dto
{
    public class OrderDetailDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal TempPrice { get; set; }
    }
}
