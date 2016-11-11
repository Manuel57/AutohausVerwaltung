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

namespace Database.Configuration
{
    // <copyright file="Database.Configuraton.DatabaseConfiguration">
    // Copyright (c) 2016 All Rights Reserved
    // <author>Manuel Lackenbucher</author>
    // <author>Thomas Huber</author>
    // </copyright>
    /// <summary>
    /// Class holding the configuration settings for the database
    /// </summary>
    public class DatabaseConfiguration
    {
            
        private string provider = DefaultConfig.OLEDB_PROVIDER;
        private string service = DefaultConfig.SERVICE_NAME;
        private IPAddress dataSource = null;
        private IDbUser user = null;
        private Type dirver =null;
        private Type dialect=null;
        
        private static DatabaseConfiguration instance;

        private DatabaseConfiguration( ) { }

        public Assembly assembly { get; set; }

        public Type Driver { get { return dirver; } }
        public Type Dialect { get { return dialect; } }

        public void registerAll(string provider, IPAddress dataSource, string service, DbUser user, Type dialect, Type driver, Assembly assembly)
        {
            this.registerAssembly(assembly);
            this.registerDataSource(dataSource);
            this.dialect = dialect;
            this.dirver = driver;
            this.registerProvider(provider);
            this.registerServicename(service);
            this.registerUser(user);
        }
        public void registerAssembly(Assembly assembly)
        {
            this.assembly = assembly;
        }
        public void registerDialect<T>( ) where T : NHibernate.Dialect.Dialect
        {
            this.dialect = typeof(T);
        }
        public void registerDriver<T>() where T : IDriver
        {
            this.dirver = typeof(T);
        }
        public void registerServicename(string servicename)
        {
            this.service = servicename;
        }
        public void registerDataSource(IPAddress dataSource)
        {
            this.dataSource = dataSource;
        }
        public void registerProvider(string provider)
        {
            this.provider = provider;
        }
        public void registerUser(IDbUser user)
        {
            this.user = user;
        }

        

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


        public Assembly GetAssembly()
        {
            return this.assembly;
        }

        internal string GetConnectionString( )
        {
            string s = string.Empty;
            try
            {
                s = string.Format("Provider={0}; Data Source={1}/{2}; User Id = {3}; Password = {4};" ,
                this.provider , this.dataSource, this.service , this.user.Username , this.user.Password);
            }
            catch ( Exception e )
            {

            }
            return s;
        }
    }
}
