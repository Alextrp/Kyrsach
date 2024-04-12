using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SessionRepository:ISessionRepository
    {
        private readonly AppContext _context;

        public SessionRepository(AppContext context)
        {
            _context = context;
        }

        public List<Session> GetAll()
        {
            return _context.Set<Session>().ToList();
        }

        public Session GetById(int id)
        {
            return _context.Set<Session>().Find(id);
        }

        public void Add(Session entity)
        {
            _context.Set<Session>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Set<Session>().Find(id);
            if (entity != null)
            {
                _context.Set<Session>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public void Update(Session entity)
        {
            var existingSession = _context.Sessions.FirstOrDefault(s => s.SessionId == entity.SessionId);
            if (existingSession != null)
            {
                _context.Entry(existingSession).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
        }

    }
}
