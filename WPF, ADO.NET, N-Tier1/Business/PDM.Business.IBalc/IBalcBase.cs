using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDM.Business.IBalc
{
    public interface IBalcBase<T>
    {
        int Update(T item);
        int Delete(int id);

        int Create(T item);

        IEnumerable<T> GetAll();
        T GetByID(int id);
    }
}
