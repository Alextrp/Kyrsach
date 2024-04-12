using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CargoRepository:ICargoRepository
    {
        private readonly AppContext _context;

        public CargoRepository(AppContext context)
        {
            _context = context;
        }

        public List<Cargo> GetAll()
        {
            return _context.Set<Cargo>()
                           .Include(c => c.CargoType)
                           .Include(c => c.Owner)
                           .ToList();
        }

        public Cargo GetById(int id)
        {
            return _context.Set<Cargo>()
                           .Include(c => c.CargoType)
                           .Include(c => c.Owner)
                           .FirstOrDefault(c => c.CargoID == id);
        }

        public void Add(Cargo entity)
        {
            _context.Set<Cargo>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Set<Cargo>().Find(id);
            if (entity != null)
            {
                _context.Set<Cargo>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public void Update(Cargo entity)
        {
            var exitingCargo = _context.Cargos.FirstOrDefault(c => c.CargoID == entity.CargoID);
            if (exitingCargo != null)
            {
                _context.Entry(exitingCargo).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
        }
    }
}
