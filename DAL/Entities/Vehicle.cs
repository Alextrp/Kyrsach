using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Vehicle
    {
        [Key]
        public int VehicleID { get; set; }

        public string Model { get; set; }

        public string LicensePlate { get; set; }

        public double CapacityWeight { get; set; }

        public double CapacityVolume { get; set; }

        public int VehicleTypeID { get; set; }
        [ForeignKey("VehicleTypeID")]
        public VehicleType VehicleType { get; set; }

        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User Driver { get; set; }
    }
}
