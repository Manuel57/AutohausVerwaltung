using NHibernate.Dialect;
using NHibernate.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Connection.Defaults
{
    // <copyright file="Database.Connection.Defaults.DefaultConfig">
    // Copyright (c) 2016 All Rights Reserved
    // <author>Manuel Lackenbucher</author>
    // <author>Thomas Huber</author>
    // </copyright>
    /// <summary>
    /// Default database configuration properties
    /// </summary>
    public class DefaultConfig
    {
        public static readonly string OLEDB_PROVIDER = "OraOLEDB.Oracle";
        public static readonly string SERVICE_NAME = "ora11g";
        public static readonly Type ORACLE_DIALECT = typeof(Oracle10gDialect);
        public static readonly Type ORACLE_DRIVER = typeof(OleDbDriver);
    }
}
