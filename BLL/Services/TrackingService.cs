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
    public class TrackingService: IService<TrackingDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrackingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<Tracking, TrackingDTO>()).CreateMapper();
        }

        public List<TrackingDTO> GetAll()
        {
            List<Tracking> trackings = _unitOfWork.Trackings.GetAll();
            return _mapper.Map<List<TrackingDTO>>(trackings);
        }

        public TrackingDTO GetById(object id)
        {
            Tracking tracking = _unitOfWork.Trackings.GetById(id);
            return _mapper.Map<TrackingDTO>(tracking);
        }

        public void Add(TrackingDTO entity)
        {
            Tracking tracking = _mapper.Map<Tracking>(entity);
            _unitOfWork.Trackings.Add(tracking);
            _unitOfWork.Save();
        }

        public void Delete(object id)
        {
            _unitOfWork.Trackings.Delete(id);
            _unitOfWork.Save();
        }

        public void Update(TrackingDTO entity)
        {
            Tracking tracking = _mapper.Map<Tracking>(entity);
            _unitOfWork.Trackings.Update(tracking);
            _unitOfWork.Save();
        }
    }
}
