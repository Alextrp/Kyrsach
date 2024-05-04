using System.ComponentModel.DataAnnotations;

namespace Kyrsach.Models
{
    public class DriverViewModel
    {
        public int OrderId { get; set; }

        [Display(Name = "Место погрузки")]
        public string PickupLocation { get; set; }

        [Display(Name = "Место разгрузки")]
        public string DropoffLocation { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата погрузки")]

        public DateTime OrderDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата доставки")]
        public DateTime DeliveryDate { get; set; }
    }
}
