using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PaymentRepository:IPaymentRepository
    {
        private readonly AppContext _context;

        public PaymentRepository(AppContext context)
        {
            _context = context;
        }

        public List<Payment> GetAll()
        {
            return _context.Set<Payment>().ToList();
        }

        public Payment GetById(int id)
        {
            return _context.Set<Payment>().Find(id);
        }

        public void Add(Payment entity)
        {
            _context.Set<Payment>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Set<Payment>().Find(id);
            if (entity != null)
            {
                _context.Set<Payment>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public void Update(Payment entity)
        {
            var existingPayment = _context.Payments.FirstOrDefault(p => p.PaymentId == entity.PaymentId);
            if (existingPayment != null)
            {
                _context.Entry(existingPayment).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
        }
    }
}
