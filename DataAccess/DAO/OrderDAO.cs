using AutoMapper;
using BusinessObject;
using DataAccess.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDAO
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
                        cfg.CreateMap<Order, OrderCreateDTO>().ReverseMap();      
                        cfg.CreateMap<OrderDetail, OrderResponseDTO>().ReverseMap();

                    });

                    _mapper = config.CreateMapper();
                }

                return _mapper;
            }
        }

        public static List<Order> GetOrders()
        {
            var listOrders = new List<Order>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    listOrders = context.Orders.ToList();
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return listOrders;
        }
        public static Order FindById(int orderId)
        {
            Order order = new Order();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    order = context.Orders.SingleOrDefault(x => x.OrderId == orderId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return order;
        }
        public static int SaveOrder(OrderCreateDTO orderCreate)
        {
            int orderRes;

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var order = Mapper.Map<Order>(orderCreate);
                    context.Orders.Add(order);
                    context.SaveChanges();

                    ProductDAO.UpdateQuantity(orderCreate);

                    orderRes = order.OrderId;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return orderRes;
        }
        public static void UpdateOrder(Order order)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Entry<Order>(order).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void DeleteOrder(Order order)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var orderFromDb = context.Orders
                        .SingleOrDefault(x => x.OrderId == order.OrderId);

                    context.Orders.Remove(orderFromDb);
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
