using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        public int CargoID { get; set; }
        [ForeignKey("CargoID")]
        public Cargo Cargo { get; set; }

        public string PickupLocation { get; set; }

        public string DropoffLocation { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        public int StatusID { get; set; }
        [ForeignKey("StatusID")]
        public OrderStatus Status { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public User Driver { get; set; }
    }
}
