using BusinessObject;
using DataAccess.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IOrderDetailRepository
    {
        void SaveOrderDetail(OrderDetail orderDetail);
        OrderDetail GetOrderDetailById(int id);
        List<ProductSalesDTO> GetProductSalesByMemeber(int memId);
        void DeleteOrderDetail(OrderDetail orderDetail);
        void UpdateOrderDetail(OrderDetail orderDetail);
        List<Member> GetMembers();
        List<OrderDetail> GetOrders();
    }
}
