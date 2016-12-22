// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-1</date>

using NHibernate.Dialect;
using NHibernate.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Connection.Defaults
{
    /// <summary>
    /// Default database configuration properties
    /// </summary>
    public class DefaultConfig
    {
        /// <summary>
        /// Default oledb provider
        /// </summary>
        public static readonly string OLEDB_PROVIDER = "OraOLEDB.Oracle";
        /// <summary>
        /// Default service name
        /// </summary>
        public static readonly string SERVICE_NAME = "ora11g";
        /// <summary>
        /// Default oracle dialect
        /// </summary>
        public static readonly Type ORACLE_DIALECT = typeof(Oracle10gDialect);
        /// <summary>
        /// Default oracle driver
        /// </summary>
        public static readonly Type ORACLE_DRIVER = typeof(OleDbDriver);
    }
}
