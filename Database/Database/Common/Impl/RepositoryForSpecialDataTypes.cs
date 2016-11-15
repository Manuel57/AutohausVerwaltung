using NHibernate;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Common.Impl
{
    //für sdo geometry 
   public class RepositoryForSpecialDataTypes : Repository
    {
              
        private RepositoryForSpecialDataTypes() { session = Database.Connection.Database.Instance.OpenSession(); }

        public void GetQuery<T>(string query)
        {
          IList<T> ret = session.CreateSQLQuery("selecte sdo_geometry from werkstatt...").AddEntity(typeof(T)).List<T>();
        }
    }
}
