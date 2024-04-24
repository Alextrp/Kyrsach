using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Kyrsach.Models
{
    public class TransportOrderViewModel
    {
        [Required]
        public string PickupLocation { get; set; }

        [Required]
        public string DropoffLocation { get; set; }

        [Required]
        public int CargoTypeID { get; set; }
        public IEnumerable<SelectListItem> CargoTypes { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Weight must be greater than zero")]
        public double Weight { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Volume must be greater than zero")]
        public double Volume { get; set; }
    }

}
