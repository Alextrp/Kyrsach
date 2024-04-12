using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Tracking
    {
        [Key]
        public int TrackingID { get; set; }

        public int OrderID { get; set; }
        [ForeignKey("OrderID")]
        public Order Order { get; set; }

        public int StatusID { get; set; }
        [ForeignKey("StatusID")]

        public OrderStatus Status { get; set; }

        public DateTime Timestamp {  get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }

    }
}
