using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic_Repository
{
    public interface IGenericrepository<T>
    {
        void Add(T item);
        T Get(int id);
        List<T> GetAll();
        void Delete(int id);
    }
}
