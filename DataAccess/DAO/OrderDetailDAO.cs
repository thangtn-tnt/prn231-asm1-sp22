using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDetailDAO
    {
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
