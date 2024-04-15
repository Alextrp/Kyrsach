using DAL.Entities;
using DAL.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TrackingRepository: ITrackingRepository
    {
        private readonly AppContext _context;

        public TrackingRepository(AppContext context)
        {
            _context = context;
        }

        public List<Tracking> GetAll()
        {
            return _context.Set<Tracking>().ToList();
        }

        public Tracking GetById(object id)
        {
            return _context.Set<Tracking>().Find(id);
        }

        public void Add(Tracking entity)
        {
            _context.Set<Tracking>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(object id)
        {
            var entity = _context.Set<Tracking>().Find(id);
            if (entity != null)
            {
                _context.Set<Tracking>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public void Update(Tracking entity)
        {
            var existingTracking = _context.Trackings.FirstOrDefault(t => t.TrackingID == entity.TrackingID);
            if (existingTracking != null)
            {
                _context.Entry(existingTracking).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
        }
    }
}
