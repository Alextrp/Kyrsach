using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Kyrsach.Models
{
    public class ManagerViewModel
    {
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

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name ="Цена")]
        public int Amount { get; set; }

        [Display(Name ="Статус оплаты")]
        public string Status { get; set; }


    }
}
