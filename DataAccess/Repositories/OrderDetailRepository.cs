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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void DeleteOrderDetail(OrderDetail orderDetail)
        {
            throw new NotImplementedException();
        }

        public List<Member> GetMembers()
        {
            throw new NotImplementedException();
        }

        public OrderDetail GetOrderDetailById(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderDetail> GetOrders()
        {
            throw new NotImplementedException();
        }

        public List<ProductSalesDTO> GetProductSalesByMemeber(int memId) => OrderDetailDAO.GetProductSalesByMember(memId);

        public void SaveOrderDetail(OrderDetail orderDetail)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            throw new NotImplementedException();
        }
    }
}
