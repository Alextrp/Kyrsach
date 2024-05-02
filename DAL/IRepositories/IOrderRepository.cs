using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IOrderRepository:IRepository<Order>
    {
        public List<Order> GetOrdersForManager(int statusId);

    }
}
