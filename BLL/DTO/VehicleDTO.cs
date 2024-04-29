using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class VehicleDTO
    {
        [Key]
        public int VehicleID { get; set; }

        public string Model { get; set; }

        public string LicensePlate { get; set; }

        public double CapacityWeight { get; set; }

        public double CapacityVolume { get; set; }

        public int VehicleTypeID { get; set; }
        [ForeignKey("VehicleTypeID")]
        public VehicleTypeDTO VehicleType { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public UserDTO User { get; set; }

    }
}
