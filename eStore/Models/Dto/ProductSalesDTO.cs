﻿using Microsoft.Data.SqlClient.Server;
using System;

namespace eStore.Models.Dto
{
    public class ProductSalesDTO
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public Nullable<DateTime> ShippedDate { get; set; }
    }
}
