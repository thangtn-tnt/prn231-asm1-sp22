using System;

namespace eStore.Models.Dto
{
    public class OrderCreateDTO
    {
        public int MemberId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}
