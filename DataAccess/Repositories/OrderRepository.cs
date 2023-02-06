using BusinessObject;
using DataAccess.DAO;
using DataAccess.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public void DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public List<Member> GetMembers()
        {
            throw new NotImplementedException();
        }

        public Order GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrders()
        {
            throw new NotImplementedException();
        }

        //public List<ProductSalesDTO> GetProductSalesByMemeber(int memId) => OrderDAO.GetProductSalesByMember(memId);

        public int SaveOrder(OrderCreateDTO order) => OrderDAO.SaveOrder(order);

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
