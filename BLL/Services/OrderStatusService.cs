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
    public class OrderStatusService: IService<OrderStatusDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderStatusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderStatus, OrderStatusDTO>()).CreateMapper();
        }

        public List<OrderStatusDTO> GetAll()
        {
            List<OrderStatus> statuses = _unitOfWork.OrderStatus.GetAll();
            return _mapper.Map<List<OrderStatusDTO>>(statuses);
        }

        public OrderStatusDTO GetById(object id)
        {
            OrderStatus status = _unitOfWork.OrderStatus.GetById(id);
            return _mapper.Map<OrderStatusDTO>(status);
        }

        public void Add(OrderStatusDTO entity)
        {
            OrderStatus status = _mapper.Map<OrderStatus>(entity);
            _unitOfWork.OrderStatus.Add(status);
            _unitOfWork.Save();
        }

        public void Delete(object id)
        {
            _unitOfWork.OrderStatus.Delete(id);
            _unitOfWork.Save();
        }

        public void Update(OrderStatusDTO entity)
        {
            OrderStatus status = _mapper.Map<OrderStatus>(entity);
            _unitOfWork.OrderStatus.Update(status);
            _unitOfWork.Save();
        }
    }
}
