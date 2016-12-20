using Database.Common;
using LagerverwaltungBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verwaltung.Exception;

namespace LagerverwaltungBL.Controller
{
    public class TeileManager
    {
        private static IRepository repository = null;

        public static IEnumerable<Autoteile> GetAutoteile( int id )
        {
            try
            {
                return null;
            }
            catch ( DatabaseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Error in selecting singel customer by id " + id) );
            }

        }
    }
}
