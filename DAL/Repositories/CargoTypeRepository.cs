using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CargoTypeRepository:ICargoTypeRepository
    {
        private readonly AppContext _context;

        public CargoTypeRepository(AppContext context)
        {
            _context = context;
        }

        public List<CargoType> GetAll()
        {
            return _context.Set<CargoType>().ToList();
        }

        public CargoType GetById(object id)
        {
            return _context.Set<CargoType>().Find(id);
        }

        public void Add(CargoType entity)
        {
            _context.Set<CargoType>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(object id)
        {
            var entity = _context.Set<CargoType>().Find(id);
            if (entity != null)
            {
                _context.Set<CargoType>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public void Update(CargoType entity)
        {
            var exitingCargoType = _context.CargoTypes.FirstOrDefault(c => c.CargoTypeID == entity.CargoTypeID);
            if (exitingCargoType != null)
            {
                _context.Entry(exitingCargoType).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
        }
    }
}
