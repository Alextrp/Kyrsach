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
    public class UserRepository:IUserRepository
    {
        private readonly AppContext _context;

        public UserRepository(AppContext userManager)
        {
            _context = userManager;
        }

        public List<User> GetAll()
        {
            return  _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Set<User>().Find(id);
        }

        public void Add(User entity)
        {
            _context.Set<User>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Set<User>().Find(id);
            if (entity != null)
            {
                _context.Set<User>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public void Update(User entity)
        {
            var existingUser = _context.Users.FirstOrDefault(t => t.Id == entity.Id);
            if (existingUser != null)
            {
                _context.Entry(existingUser).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
        }
    }
}
