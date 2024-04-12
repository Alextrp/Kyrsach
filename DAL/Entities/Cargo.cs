using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Cargo
    {
        public int CargoID { get; set; }

        public string Description { get; set; }

        public double Weight { get; set; }

        public double Volume { get; set; }

        public int CargoTypeID { get; set; }
        [ForeignKey("CargoTypeID")]
        public CargoType CargoType { get; set; }

        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User Owner { get; set; }
    }
}
