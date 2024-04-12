using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ReviewRepository:IReviewRepository
    {
        private readonly AppContext _context;

        public ReviewRepository(AppContext context)
        {
            _context = context;
        }

        public List<Review> GetAll()
        {
            return _context.Set<Review>().ToList();
        }

        public Review GetById(int id)
        {
            return _context.Set<Review>().Find(id);
        }

        public void Add(Review entity)
        {
            _context.Set<Review>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Set<Review>().Find(id);
            if (entity != null)
            {
                _context.Set<Review>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public void Update(Review entity)
        {
            var existingReview = _context.Reviews.FirstOrDefault(r => r.ReviewID == entity.ReviewID);
            if (existingReview != null)
            {
                _context.Entry(existingReview).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
        }
    }
}
