using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderRepository:IOrderRepository
    {
        private readonly AppContext _context;

        public OrderRepository(AppContext context)
        {
            _context = context;
        }

        public List<Order> GetAll()
        {
            return _context.Set<Order>().ToList();
        }

        public Order GetById(object id)
        {
            return _context.Set<Order>().Find(id);
        }

        public List<Order> GetOrdersForManager(int statusId)
        {
            return _context.Set<Order>().Where(o => o.StatusID == statusId).ToList();
        }

        public void Add(Order entity)
        {
            _context.Set<Order>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(object id)
        {
            var entity = _context.Set<Order>().Find(id);
            if (entity != null)
            {
                _context.Set<Order>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public void Update(Order entity)
        {
            var existingOrder = _context.Orders.FirstOrDefault(o => o.OrderID == entity.OrderID);
            if (existingOrder != null)
            {
                _context.Entry(existingOrder).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
        }
    }
}
