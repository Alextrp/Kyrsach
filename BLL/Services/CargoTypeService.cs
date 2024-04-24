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
    public class CargoTypeService : IService<CargoTypeDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CargoTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<CargoType, CargoTypeDTO>().ReverseMap()).CreateMapper();
        }

        public List<CargoTypeDTO> GetAll()
        {
            List<CargoType> cargoTypes = _unitOfWork.CargoTypes.GetAll();
            return _mapper.Map<List<CargoTypeDTO>>(cargoTypes);
        }

        public CargoTypeDTO GetById(object id)
        {
            CargoType cargoType = _unitOfWork.CargoTypes.GetById(id);
            return _mapper.Map<CargoTypeDTO>(cargoType);
        }

        public void Add(CargoTypeDTO entity)
        {
            CargoType cargoType = _mapper.Map<CargoType>(entity);
            _unitOfWork.CargoTypes.Add(cargoType);
            _unitOfWork.Save();
        }

        public void Delete(object id)
        {
            _unitOfWork.CargoTypes.Delete(id);
            _unitOfWork.Save();
        }

        public void Update(CargoTypeDTO entity)
        {
            CargoType cargoType = _mapper.Map<CargoType>(entity);
            _unitOfWork.CargoTypes.Update(cargoType);
            _unitOfWork.Save();
        }
    }
}
