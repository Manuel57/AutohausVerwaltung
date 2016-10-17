using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDM.Data.Dalc
{
    internal class PDMDatabase
    {
        /// <summary>
        /// The connection string for the Product Data Management Database
        /// </summary>
        internal static string DatabaseConnectionString = ConfigurationManager.ConnectionStrings["PDMContext"].ConnectionString;
        /// <summary>
        /// Prevent object construction
        /// </summary>
        private PDMDatabase()
        { }
    }
}
