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
    public class UserService : IService<UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
        }

        public List<UserDTO> GetAll()
        {
            List<User> users = _unitOfWork.Users.GetAll();
            return _mapper.Map<List<UserDTO>>(users);
        }

        public UserDTO GetById(object id)
        {
            User user = _unitOfWork.Users.GetById(id);
            return _mapper.Map<UserDTO>(user);
        }

        public void Add(UserDTO entity)
        {
            User user = _mapper.Map<User>(entity);
            _unitOfWork.Users.Add(user);
            _unitOfWork.Save();
        }

        public void Delete(object id)
        {
            _unitOfWork.Users.Delete(id);
            _unitOfWork.Save();
        }

        public void Update(UserDTO entity)
        {
            User user = _mapper.Map<User>(entity);
            _unitOfWork.Users.Update(user);
            _unitOfWork.Save();
        }
    }
}
