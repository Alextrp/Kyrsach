using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class CargoType
    {
        [Key]
        public int CargoTypeID { get; set; }

        public string TypeName { get; set; }

        public string Description { get; set; }
    }
}
