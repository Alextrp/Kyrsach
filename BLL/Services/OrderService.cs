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
    public class OrderService: IService<OrderDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>().ReverseMap()).CreateMapper();
        }

        public List<OrderDTO> GetAll()
        {
            List<Order> orders = _unitOfWork.Orders.GetAll();
            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public OrderDTO GetById(object id)
        {
            Order order = _unitOfWork.Orders.GetById(id);
            return _mapper.Map<OrderDTO>(order);
        }

        public void Add(OrderDTO entity)
        {
            Order order = _mapper.Map<Order>(entity);
            _unitOfWork.Orders.Add(order);
            _unitOfWork.Save();
        }

        public void Delete(object id)
        {
            _unitOfWork.Orders.Delete(id);
            _unitOfWork.Save();
        }

        public void Update(OrderDTO entity)
        {
            Order order = _mapper.Map<Order>(entity);
            _unitOfWork.Orders.Update(order);
            _unitOfWork.Save();
        }
    }
}
