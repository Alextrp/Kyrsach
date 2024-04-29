using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        public int OrderID { get; set; }

        [ForeignKey("OrderID")]
        public Order Order { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        public decimal Rating {  get; set; }
        public string Comment {  get; set; }
        public DateTime ReviewDate { get; set;}
    }
}
