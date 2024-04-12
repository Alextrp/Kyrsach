using DAL.Entities;
using DAL.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppContext _context;

        public VehicleRepository(AppContext context)
        {
            _context = context;
        }

        public List<Vehicle> GetAll()
        {
            return _context.Vehicles
                           .Include(v => v.VehicleType) // Загрузка связанного типа транспорта
                           .Include(v => v.Driver) // Загрузка связанного водителя
                           .ToList();
        }

        public Vehicle GetById(int id)
        {
            return _context.Vehicles
                           .Include(v => v.VehicleType)
                           .Include(v => v.Driver)
                           .FirstOrDefault(v => v.VehicleID == id);
        }

        public void Add(Vehicle entity)
        {
            _context.Vehicles.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var vehicle = _context.Vehicles.Find(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                _context.SaveChanges();
            }
        }

        public void Update(Vehicle entity)
        {
            var existingVehicle = _context.Vehicles.FirstOrDefault(v => v.VehicleID == entity.VehicleID);
            if (existingVehicle != null)
            {
                _context.Entry(existingVehicle).CurrentValues.SetValues(entity);
                // Обновляем водителя и тип транспорта, если это необходимо
                if (existingVehicle.VehicleTypeID != entity.VehicleTypeID)
                    existingVehicle.VehicleType = _context.VehicleTypes.Find(entity.VehicleTypeID);

                if (existingVehicle.UserID != entity.UserID)
                    existingVehicle.Driver = _context.Users.Find(entity.UserID);

                _context.SaveChanges();
            }
        }
    }
}
