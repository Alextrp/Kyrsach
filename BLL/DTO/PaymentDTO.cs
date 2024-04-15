using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; }

        public int OrderID { get; set; }

        public int Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }

        public PaymentDTO() { }
        public PaymentDTO(int paymentId, int orderID, int amount, DateTime paymentDate, string paymentMethod, string status)
        {
            PaymentId = paymentId;
            OrderID = orderID;
            Amount = amount;
            PaymentDate = paymentDate;
            PaymentMethod = paymentMethod;
            Status = status;
        }
    }
}
