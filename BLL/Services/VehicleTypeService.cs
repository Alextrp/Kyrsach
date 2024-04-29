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
    public class VehicleTypeService: IService<VehicleTypeDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VehicleTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<VehicleType, VehicleTypeDTO>().ReverseMap()).CreateMapper();
        }

        public List<VehicleTypeDTO> GetAll()
        {
            List<VehicleType> vehicleTypes = _unitOfWork.VehicleTypes.GetAll();
            return _mapper.Map<List<VehicleTypeDTO>>(vehicleTypes);
        }

        public VehicleTypeDTO GetById(object id)
        {
            VehicleType vehicleType = _unitOfWork.VehicleTypes.GetById(id);
            return _mapper.Map<VehicleTypeDTO>(vehicleType);
        }

        public void Add(VehicleTypeDTO entity)
        {
            VehicleType vehicleType = _mapper.Map<VehicleType>(entity);
            _unitOfWork.VehicleTypes.Add(vehicleType);
            _unitOfWork.Save();
        }

        public void Delete(object id)
        {
            _unitOfWork.VehicleTypes.Delete(id);
            _unitOfWork.Save();
        }

        public void Update(VehicleTypeDTO entity)
        {
            VehicleType vehicleType = _mapper.Map<VehicleType>(entity);
            _unitOfWork.VehicleTypes.Update(vehicleType);
            _unitOfWork.Save();
        }
    }
}
