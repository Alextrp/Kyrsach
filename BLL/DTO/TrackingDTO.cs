using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class TrackingDTO
    {
        public int TrackingID { get; set; }

        public int OrderID { get; set; }
        

        public int StatusID { get; set; }
       

        public DateTime Timestamp { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }

        public TrackingDTO() { }
        public TrackingDTO(int trackingID, int orderID, int statusID, DateTime timestamp, string location, string notes)
        {
            TrackingID = trackingID;
            OrderID = orderID;
            StatusID = statusID;
            Timestamp = timestamp;
            Location = location;
            Notes = notes;
        }
    }
}
