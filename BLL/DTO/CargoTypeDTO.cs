using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class CargoTypeDTO
    {
        public int CargoTypeID { get; set; }

        public string TypeName { get; set; }

        public string Description { get; set; }

        public CargoTypeDTO() { }
        public CargoTypeDTO(int cargoTypeID, string typeName, string description)
        {
            CargoTypeID = cargoTypeID;
            TypeName = typeName;
            Description = description;
        }
    }
}
