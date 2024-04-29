using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ReviewService: IService<ReviewDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<Review, ReviewDTO>().ReverseMap()).CreateMapper();
        }

        public List<ReviewDTO> GetAll()
        {
            List<Review> reviews = _unitOfWork.Reviews.GetAll();
            return _mapper.Map<List<ReviewDTO>>(reviews);
        }

        public ReviewDTO GetById(object id)
        {
            Review review = _unitOfWork.Reviews.GetById(id);
            return _mapper.Map<ReviewDTO>(review);
        }

        public void Add(ReviewDTO entity)
        {
            Review review = _mapper.Map<Review>(entity);
            _unitOfWork.Reviews.Add(review);
            _unitOfWork.Save();
        }

        public void Delete(object id)
        {
            _unitOfWork.Reviews.Delete(id);
            _unitOfWork.Save();
        }

        public void Update(ReviewDTO entity)
        {
            Review review = _mapper.Map<Review>(entity);
            _unitOfWork.Reviews.Update(review);
            _unitOfWork.Save();
        }
    }
}
