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
    public class VehicleService: IService<VehicleDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VehicleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<Vehicle, VehicleDTO>().ReverseMap()).CreateMapper();
        }

        public List<VehicleDTO> GetAll()
        {
            List<Vehicle> vehicles = _unitOfWork.Vehicles.GetAll();
            return _mapper.Map<List<VehicleDTO>>(vehicles);
        }

        public VehicleDTO GetById(object id)
        {
            Vehicle vehicle = _unitOfWork.Vehicles.GetById(id);
            return _mapper.Map<VehicleDTO>(vehicle);
        }

        public void Add(VehicleDTO entity)
        {
            Vehicle vehicle = _mapper.Map<Vehicle>(entity);
            _unitOfWork.Vehicles.Add(vehicle);
            _unitOfWork.Save();
        }

        public void Delete(object id)
        {
            _unitOfWork.Vehicles.Delete(id);
            _unitOfWork.Save();
        }

        public void Update(VehicleDTO entity)
        {
            Vehicle vehicle = _mapper.Map<Vehicle>(entity);
            _unitOfWork.Vehicles.Update(vehicle);
            _unitOfWork.Save();
        }
    }
}
