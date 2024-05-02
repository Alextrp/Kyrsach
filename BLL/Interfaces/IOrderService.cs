using BLL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IOrderService: IService<OrderDTO>
    {
        public List<OrderDTO> GetOrdersForManager(int statusId);
    }
}
