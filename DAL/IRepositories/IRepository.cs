﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(object id);
        void Add(T entity);
        void Delete(object id);
        void Update(T entity);
    }
}
