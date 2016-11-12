// <copyright file="Database.Configuration.databaseconfiguration.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-11</date>
// </copyright>

using Database.Connection;
using Database.Connection.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Driver;
using NHibernate.Dialect;
using System.Reflection;
using System.Net;
using Database.Common.ObserverPattern;

namespace Database.Configuration
{
    /// <summary>
    /// Class holding the configuration settings for the database
    /// </summary>
    public class DatabaseConfiguration : Subject
    {
        #region fields
        /// <summary>
        /// The provider
        /// </summary>
        private string provider = DefaultConfig.OLEDB_PROVIDER;
        /// <summary>
        /// The service name
        /// </summary>
        private string service = DefaultConfig.SERVICE_NAME;
        /// <summary>
        /// The data source
        /// </summary>
        private IPAddress dataSource = null;
        /// <summary>
        /// The user containing usernme and password
        /// </summary>
        private IDbUser user = null;
        /// <summary>
        /// The driver
        /// </summary>
        private Type dirver =null;
        /// <summary>
        /// The dialect
        /// </summary>
        private Type dialect=null;

        /// <summary>
        /// The instance
        /// </summary>
        private static DatabaseConfiguration instance;

        #endregion
        #region constructor
        /// <summary>
        /// The constructor
        /// subscribes the database instance as observer
        /// </summary>
        private DatabaseConfiguration( )
        {
            Subscribe(Database.Connection.Database.Instance);
        }
        #endregion
        #region properties
        /// <summary>
        /// gets or sets the assembly
        /// </summary>
        public Assembly assembly { get; set; }
        /// <summary>
        /// Gets the driver
        /// </summary>
        public Type Driver { get { return dirver; } }
        /// <summary>
        /// gets the dialect
        /// </summary>
        public Type Dialect { get { return dialect; } }
        /// <summary>
        /// Gets the instance
        /// </summary>
        public static DatabaseConfiguration Instance
        {
            get
            {
                if ( instance == null )
                {
                    instance = new DatabaseConfiguration();
                }
                return instance;
            }
        }


        #endregion
        #region methods
        /// <summary>
        /// returns the assembly
        /// </summary>
        /// <returns>the assembly</returns>
        public Assembly GetAssembly( )
        {
            return this.assembly;
        }
        /// <summary>
        /// Notifies the observers
        /// </summary>
        public void Updated( )
        {
            this.Notify();
        }

        #region register methods
        /// <summary>
        /// sets all the attributes
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <param name="dataSource">The date source</param>
        /// <param name="service">The service name</param>
        /// <param name="user">The user containing the user data</param>
        /// <param name="dialect">The dialect</param>
        /// <param name="driver">The dirver</param>
        /// <param name="assembly">The assembly</param>
        public void RegisterAll( string provider , IPAddress dataSource , string service , DbUser user , Type dialect , Type driver , Assembly assembly )
        {
            this.RegisterAssembly(assembly);
            this.RegisterDataSource(dataSource);
            this.dialect = dialect;
            this.dirver = driver;
            this.RegisterProvider(provider);
            this.RegisterServicename(service);
            this.RegisterUser(user);
            this.Notify();
        }
        public void RegisterAssembly( Assembly assembly )
        {
            this.assembly = assembly;
        }
        public void RegisterDialect<T>( ) where T : NHibernate.Dialect.Dialect
        {
            this.dialect = typeof(T);
        }
        public void RegisterDriver<T>( ) where T : IDriver
        {
            this.dirver = typeof(T);
        }
        public void RegisterServicename( string servicename )
        {
            this.service = servicename;
        }
        public void RegisterDataSource( IPAddress dataSource )
        {
            this.dataSource = dataSource;
        }
        public void RegisterProvider( string provider )
        {
            this.provider = provider;
        }
        public void RegisterUser( IDbUser user )
        {
            this.user = user;
        }

        #endregion

        /// <summary>
        /// Returns s connection string
        /// </summary>
        /// <returns>connection string</returns>
        internal string GetConnectionString( )
        {
            string s = string.Empty;


            try
            {
                s = string.Format("Provider={0}; Data Source={1}/{2}; User Id = {3}; Password = {4};" ,
            this.provider , this.dataSource , this.service , this.user.Username , this.user.Password);


            }
            catch ( DatabaseException ex )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Error at creating the connectin string" , s) );
            }


            return s;
        }
        #endregion

    }
}
