using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using eStore.Models.Dto;
using eStore.Services;
using System.Globalization;

namespace eStore.Controllers
{
    public class SalesController : Controller
    {        
        public SalesController()
        {
     
        }
        public IActionResult Index()
        {
            return View();
        }
        public List<ProductSalesDTO> Generate(DateTime startDate, DateTime endDate)
        {
            //var orderDetails = _service.OrderDetails
            //    .Where(od => od.Order.OrderDate >= startDate && od.Order.OrderDate <= endDate)
            //    .ToList();

            //var salesStatistics = orderDetails
            //    .GroupBy(od => od.Product.ProductName)
            //    .Select(group => new SalesStatistics
            //    {
            //        ProductName = group.Key,
            //        TotalSales = group.Sum(od => od.UnitPrice * od.Quantity * (1 - od.Discount))
            //    })
            //    .OrderByDescending(s => s.TotalSales)
            //    .ToList();

            var salesStatistics = new List<ProductSalesDTO>();  

            return salesStatistics;
        }

        public IActionResult SalesReport(string startDate, string endDate)
        {
            DateTime parsedStartDate = DateTime.ParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            DateTime parsedEndDate = DateTime.ParseExact(endDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            //var sales = _dbContext.Orders
            //    .Where(o => o.OrderDate >= parsedStartDate && o.OrderDate <= parsedEndDate)
            //    .Join(_dbContext.OrderDetails, o => o.OrderId, od => od.OrderId, (o, od) => new { Order = o, OrderDetail = od })
            //    .Join(_dbContext.Products, od => od.OrderDetail.ProductId, p => p.ProductId, (od, p) => new { OrderDetail = od.OrderDetail, Product = p })
            //    .GroupBy(result => result.Product.ProductId)
            //    .Select(g => new
            //    {            
            //        ProductName = g.First().Product.ProductName,
            //        TotalSales = g.Sum(x => x.OrderDetail.UnitPrice * x.OrderDetail.Quantity * (1 - x.OrderDetail.Discount))
            //        TotalProductsSold = g.Sum(od => od.Quantity)
            //    })
            //    .OrderByDescending(s => s.TotalSales)
            //    .ToList();
            
            return View(parsedEndDate);
        }
    }
}
