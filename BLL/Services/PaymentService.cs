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
    public class PaymentService: IService<PaymentDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<Payment, PaymentDTO>().ReverseMap()).CreateMapper();
        }

        public List<PaymentDTO> GetAll()
        {
            List<Payment> payments = _unitOfWork.Payments.GetAll();
            return _mapper.Map<List<PaymentDTO>>(payments);
        }

        public PaymentDTO GetById(object id)
        {
            Payment payment = _unitOfWork.Payments.GetById(id);
            return _mapper.Map<PaymentDTO>(payment);
        }

        public void Add(PaymentDTO entity)
        {
            Payment payment = _mapper.Map<Payment>(entity);
            _unitOfWork.Payments.Add(payment);
            _unitOfWork.Save();
        }

        public void Delete(object id)
        {
            _unitOfWork.Payments.Delete(id);
            _unitOfWork.Save();
        }

        public void Update(PaymentDTO entity)
        {
            Payment payment = _mapper.Map<Payment>(entity);
            _unitOfWork.Payments.Update(payment);
            _unitOfWork.Save();
        }
    }
}
