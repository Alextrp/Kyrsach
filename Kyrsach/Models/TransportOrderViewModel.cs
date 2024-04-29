
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Kyrsach.Models
{
    public class TransportOrderViewModel
    {
        [Required]
        [Display(Name = "Место погрузки")]
        public string PickupLocation { get; set; }

        [Required]
        [Display(Name = "Место разгрузки")]
        public string DropoffLocation { get; set; }

        [Required]
        public int CargoTypeID { get; set; }
        public IEnumerable<SelectListItem> CargoTypes { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата погрузки")]
        
        public DateTime OrderDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата доставки")]
        
        public DateTime DeliveryDate { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Вес")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Weight must be greater than zero")]
        public double Weight { get; set; }

        [Required]
        [Display(Name = "Объем")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Volume must be greater than zero")]
        public double Volume { get; set; }

        public string Distance { get; set; }
        public DateTime CurrentDate { get; } = DateTime.Now.Date;
    }

}
