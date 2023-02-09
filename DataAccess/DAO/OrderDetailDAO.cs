using AutoMapper;
using BusinessObject;
using DataAccess.Dto;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
                        cfg.CreateMap<OrderDetailCreateDTO, OrderDetailDTO>().ReverseMap();
                        cfg.CreateMap<OrderDetailCreateDTO, OrderDetail>().ReverseMap();
                        cfg.CreateMap<OrderCreateDTO, OrderDetailCreateDTO>().ReverseMap();
                        cfg.CreateMap<OrderDetail, OrderResponseDTO>().ReverseMap();

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
                                  TotalPrice = x.product.UnitPrice * x.orderDetail.Quantity * (1 - (x.orderDetail.Discount / 100)),
                                  OrderDate = x.order.OrderDate,
                                  ShippedDate = (DateTime)x.order.ShippedDate,
                                  RequiredDate= x.order.RequiredDate,
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

        public static List<ProductSalesDTO> GetProductSales (string startDate, string endDate)
        {
            var listSales = new List<ProductSalesDTO>();

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var orderHistory = context.Orders
                              .Join(context.OrderDetails, order => order.OrderId, orderDetail => orderDetail.OrderId, (order, orderDetail) => new { order, orderDetail })
                              .Join(context.Products.Include("Category"), x => x.orderDetail.ProductId, product => product.ProductId, (x, product) => new { x.order, x.orderDetail, product })
                              .Where(x => x.order.OrderDate >= DateTime.Parse(startDate) && x.order.OrderDate <= DateTime.Parse(endDate)).
                              OrderByDescending(x => x.order.OrderDate).ThenByDescending(x => x.product.UnitPrice * x.orderDetail.Quantity * (1 - (x.orderDetail.Discount / 100)))
                              .Select(x => new ProductSalesDTO
                              {
                                  OrderId = x.order.OrderId,
                                  ProductId = x.product.ProductId,
                                  ProductName = x.product.ProductName,
                                  CategoryName = x.product.Category.CategoryName,
                                  Quantity = x.orderDetail.Quantity,
                                  UnitPrice = x.product.UnitPrice,
                                  Discount = x.orderDetail.Discount,
                                  TotalPrice = x.product.UnitPrice * x.orderDetail.Quantity * (1 - (x.orderDetail.Discount / 100)),
                                  OrderDate = x.order.OrderDate,
                                  ShippedDate = (DateTime)x.order.ShippedDate,
                                  RequiredDate = x.order.RequiredDate,
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
        public static void SaveOrderDetail(OrderCreateDTO orderCreate, int orderId)
        {

            var orderDetailCreate = Mapper.Map<OrderDetailCreateDTO>(orderCreate);
            orderDetailCreate.OrderId = orderId;        
            orderDetailCreate.UnitPrice = ProductDAO.FindById(orderCreate.ProductId).UnitPrice;

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.OrderDetails.Add(Mapper.Map<OrderDetail>(orderDetailCreate));
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static OrderResponseDTO GetOrderDetail(int orderId)
        {
            var orderDetail = new OrderResponseDTO();

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    orderDetail = context.Orders
                        .Join(context.OrderDetails, o => o.OrderId, od => od.OrderId, (o, od) => new { o, od })
                        .Join(context.Products, ood => ood.od.ProductId, p => p.ProductId, (ood, p) => new { ood.o, ood.od, p })
                        .Join(context.Categories, op => op.p.CategoryId, c => c.CategoryId, (op, c) => new { op.o, op.od, op.p, c })
                        .Join(context.Members, opc => opc.o.MemberId, s => s.MemberId, (opc, s) => new { opc.o, opc.od, opc.p, opc.c, s })
                        .Select(opcs => new OrderResponseDTO
                        {
                            OrderId = opcs.o.OrderId,
                            ProductName = opcs.p.ProductName,
                            CategoryName = opcs.c.CategoryName,
                            TotalPrice = opcs.od.Quantity * opcs.od.UnitPrice * (1 - opcs.od.Discount / 100),
                            CompanyName = opcs.s.CompanyName,
                            City = opcs.s.City,
                            Country = opcs.s.Country,
                            OrderDate = opcs.o.OrderDate,
                            RequiredDate = opcs.o.RequiredDate,
                            ShippedDate = opcs.o.ShippedDate
                        }).Where(x => x.OrderId == orderId).SingleOrDefault();
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return orderDetail;
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
    }
}
