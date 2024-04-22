using DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kyrsach.Models
{
    public class CargoViewModel
    {
       
        public int CargoID { get; set; }

        public string Description { get; set; }

        public double Weight { get; set; }

        public double Volume { get; set; }

        public int CargoTypeID { get; set; }

        public string UserID { get; set; }

    }
}
