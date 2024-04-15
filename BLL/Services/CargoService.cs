using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CargoService: IService<CargoDTO>
    {
        IUnitOfWork DB { get; set; }
        private readonly IMapper _mapper;

        public CargoService(IUnitOfWork uow)
        {
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<Cargo, CargoDTO>()).CreateMapper();
            DB = uow;
        }

        public List<CargoDTO> GetAll()
        {
            List<Cargo> cargos = DB.Cargos.GetAll();
            return _mapper.Map<List<CargoDTO>>(cargos).ToList();
        }

        public void Add(CargoDTO obj)
        {
            DB.Cargos.Add(_mapper.Map<Cargo>(obj));
            DB.Save();
        }

        public void Update(CargoDTO obj)
        {
            DB.Cargos.Update(_mapper.Map<Cargo>(obj));
            DB.Save();
        }

        public void Delete(object id)
        {
            DB.Cargos.Delete(id);
            DB.Save();
        }

        public CargoDTO GetById(object id)
        {
            Cargo cargo = DB.Cargos.GetById(id);

            return _mapper?.Map<CargoDTO>(cargo);
        }

    }
}
