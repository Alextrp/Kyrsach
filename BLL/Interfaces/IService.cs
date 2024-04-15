using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IService<T> where T : class
    {
        List<T> GetAll();
        T GetById(object id);
        void Add(T entity);
        void Delete(object id);
        void Update(T entity);
    }
}
