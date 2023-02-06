﻿using System;

namespace eStore.Models.Dto
{
    public class OrderResponseDTO
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public decimal TotalPrice { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public Nullable<DateTime> ShippedDate { get; set; }
    }
}
