using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Common
{
    public interface IRepository<T> where T : IEntity
    {
        void Save(T entity);
        void Delete(T entity);
        T GetById(object objId);

        IQueryable<TEntity> ToList<TEntity>();
    }
}
