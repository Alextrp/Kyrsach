using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ReviewDTO
    {
        public int ReviewID { get; set; }

        public int OrderID { get; set; }

        public string UserID { get; set; }

        public decimal Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        public ReviewDTO() { }
        public ReviewDTO(int reviewID, int orderID, string userID, decimal rating, string comment, DateTime reviewDate)
        {
            ReviewID = reviewID;
            OrderID = orderID;
            UserID = userID;
            Rating = rating;
            Comment = comment;
            ReviewDate = reviewDate;
        }
    }
}
