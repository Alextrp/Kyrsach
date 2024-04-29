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
    public class SessionService: IService<SessionDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<Session, SessionDTO>().ReverseMap()).CreateMapper();
        }

        public List<SessionDTO> GetAll()
        {
            List<Session> sessions = _unitOfWork.Sessions.GetAll();
            return _mapper.Map<List<SessionDTO>>(sessions);
        }

        public SessionDTO GetById(object id)
        {
            Session session = _unitOfWork.Sessions.GetById(id);
            return _mapper.Map<SessionDTO>(session);
        }

        public void Add(SessionDTO entity)
        {
            Session session = _mapper.Map<Session>(entity);
            _unitOfWork.Sessions.Add(session);
            _unitOfWork.Save();
        }

        public void Delete(object id)
        {
            _unitOfWork.Sessions.Delete(id);
            _unitOfWork.Save();
        }

        public void Update(SessionDTO entity)
        {
            Session session = _mapper.Map<Session>(entity);
            _unitOfWork.Sessions.Update(session);
            _unitOfWork.Save();
        }
    }
}
