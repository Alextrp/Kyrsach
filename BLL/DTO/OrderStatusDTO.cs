using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class OrderStatusDTO
    {
        public int StatusID { get; set; }

        public string StatusName { get; set; }

        public string Description { get; set; }

        public OrderStatusDTO() { }
        public OrderStatusDTO(int statusID, string statusName, string description)
        {
            StatusID = statusID;
            StatusName = statusName;
            Description = description;
        }
    }
}
