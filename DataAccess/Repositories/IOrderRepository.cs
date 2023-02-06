using BusinessObject;
using DataAccess.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IOrderRepository
    {
        int SaveOrder(OrderCreateDTO order);
        Order GetOrderById(int id);
        void DeleteOrder(Order order);
        void UpdateOrder(Order order);                
    }
}
