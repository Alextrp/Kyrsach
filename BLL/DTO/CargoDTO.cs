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
    public class CargoDTO
    {
        [Key]
        public int CargoID { get; set; }

        public string Description { get; set; }

        public double Weight { get; set; }

        public double Volume { get; set; }

        public int CargoTypeID { get; set; }
        [ForeignKey("CargoTypeID")]
        public CargoType CargoType { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }


        public CargoDTO() { }
        public CargoDTO(int cargoID, string description, double weight, double volume, int cargoTypeID, string userID)
        {
            CargoID = cargoID;
            Description = description;
            Weight = weight;
            Volume = volume;
            CargoTypeID = cargoTypeID;
            UserID = userID;
        }


    }
}
