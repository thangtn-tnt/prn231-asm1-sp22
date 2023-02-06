using AutoMapper;
using BusinessObject;
using DataAccess.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDetailDAO
    {
        private static IMapper _mapper;
        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<ProductSalesDTO, OrderDetailDAO>().ReverseMap();
                        cfg.CreateMap<ProductSalesDTO, ProductDTO>().ReverseMap();
                        cfg.CreateMap<ProductSalesDTO, OrderDTO>().ReverseMap();
                  
                    });

                    _mapper = config.CreateMapper();
                }

                return _mapper;
            }
        }
        public static List<OrderDetail> GetOrderDetails()
        {
            var listOrderDetails = new List<OrderDetail>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    listOrderDetails = context.OrderDetails.ToList();
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return listOrderDetails;
        }
        public static List<ProductSalesDTO> GetProductSalesByMember(int memberId)
        {
            var listSales = new List<ProductSalesDTO>();

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var orderHistory = context.Orders
                              .Join(context.OrderDetails, order => order.OrderId, orderDetail => orderDetail.OrderId, (order, orderDetail) => new { order, orderDetail })
                              .Join(context.Products.Include("Category"), x => x.orderDetail.ProductId, product => product.ProductId, (x, product) => new { x.order, x.orderDetail, product })
                              .Where(x => x.order.MemberId == memberId)
                              .Select(x => new ProductSalesDTO
                              {
                                  OrderId = x.order.OrderId,
                                  ProductId = x.product.ProductId,
                                  ProductName = x.product.ProductName,
                                  CategoryName = x.product.Category.CategoryName,
                                  Quantity = x.orderDetail.Quantity,
                                  UnitPrice = x.product.UnitPrice,
                                  Discount = x.orderDetail.Discount,                                  
                                  TotalPrice = x.product.UnitPrice * x.orderDetail.Quantity * (1 - (x.orderDetail.Discount / 100))
                              });

                    var result = orderHistory.Select(x => Mapper.Map<ProductSalesDTO>(x)).ToList();
                    listSales = Mapper.Map<List<ProductSalesDTO>>(result);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return listSales;
        }
        public static OrderDetail FindById(int prodId)
        {
            OrderDetail orderDetail = new OrderDetail();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    orderDetail = context.OrderDetails.SingleOrDefault(x => x.ProductId == prodId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return orderDetail;
        }
        public static void SaveOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.OrderDetails.Add(orderDetail);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void UpdateOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Entry<OrderDetail>(orderDetail).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //public static void DeleteOrderDetail(OrderDetail orderDetail)
        //{
        //    try
        //    {
        //        using (var context = new ApplicationDbContext())
        //        {
        //            var prodFromDb = context.OrderDetails
        //                .SingleOrDefault(x => x.OrderDetailId == orderDetail.OrderDetailId);

        //            context.OrderDetails.Remove(prodFromDb);
        //            context.SaveChanges();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
    }
}
