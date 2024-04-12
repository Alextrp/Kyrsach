using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderStatusRepository: IOrderStatusRepository
    {
        private readonly AppContext _context;

        public OrderStatusRepository(AppContext context)
        {
            _context = context;
        }

        public List<OrderStatus> GetAll()
        {
            return _context.Set<OrderStatus>().ToList();
        }

        public OrderStatus GetById(int id)
        {
            return _context.Set<OrderStatus>().Find(id);
        }

        public void Add(OrderStatus entity)
        {
            _context.Set<OrderStatus>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Set<OrderStatus>().Find(id);
            if (entity != null)
            {
                _context.Set<OrderStatus>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public void Update(OrderStatus entity)
        {
            var existingStatus = _context.OrderStatuses.FirstOrDefault(s => s.StatusID == entity.StatusID);
            if (existingStatus != null)
            {
                _context.Entry(existingStatus).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
        }
    }
}
