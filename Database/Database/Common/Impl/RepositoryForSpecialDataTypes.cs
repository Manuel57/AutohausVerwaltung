using NHibernate;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Common.Impl
{
    //für sdo geometry 
    public class RepositoryForSpecialDataTypes : Repository
    {

        private RepositoryForSpecialDataTypes( ) { session = Database.Connection.Database.Instance.OpenSession(); }


        public IList GetQuery( string table, string whereClause , HashSet<Column> colums )
        {


            string columsFormat = string.Empty;
            columsFormat = string.Join(", " , colums.ToArray().Select(item => item.Name + ( ( !string.IsNullOrEmpty(item.Alias) ) ? " " + item.Alias : "" )));
            string selectString = "select {0} from {1} {2}";


            ISQLQuery query = session.CreateSQLQuery(string.Format(selectString , columsFormat , table , string.IsNullOrEmpty(whereClause) ? string.Empty : "where " +whereClause));
            foreach ( var item in colums )
            {
                if ( item.Type != null )
                    query.AddScalar(( !string.IsNullOrEmpty(item.Alias) ) ? item.Alias : item.Name , item.Type);
            }
            return query.List();

        }
    }
}
