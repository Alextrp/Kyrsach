using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class VehicleTypeRepository:IVehicleTypeRepository
    {
        private readonly AppContext _context;

        public VehicleTypeRepository(AppContext context)
        {
            _context = context;
        }

        public List<VehicleType> GetAll()
        {
            return _context.VehicleTypes.ToList();
        }

        public VehicleType GetById(int id)
        {
            return _context.VehicleTypes.FirstOrDefault(vt => vt.VehicleTypeID == id);
        }

        public void Add(VehicleType entity)
        {
            _context.VehicleTypes.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var vehicleType = _context.VehicleTypes.Find(id);
            if (vehicleType != null)
            {
                _context.VehicleTypes.Remove(vehicleType);
                _context.SaveChanges();
            }
        }

        public void Update(VehicleType entity)
        {
            var existingVehicleType = _context.VehicleTypes.FirstOrDefault(vt => vt.VehicleTypeID == entity.VehicleTypeID);
            if (existingVehicleType != null)
            {
                _context.Entry(existingVehicleType).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
        }
    }
}
