namespace eStore.Models.Dto
{
    public class OrderDetailDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}
