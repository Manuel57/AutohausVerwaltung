// <copyright file="Database.Connection.database.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-11</date>
// </copyright>

using Database.Common.ObserverPattern;
using Database.Configuration;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Connection
{
    /// <summary>
    /// The database
    /// </summary>
    public class Database : IObserver
    {
        #region fields
        /// <summary>
        /// the datase instance
        /// </summary>
        private static Database instance = null;
        /// <summary>
        /// the sessionfactory
        /// </summary>
        private ISessionFactory _sessionFactory;

        /// <summary>
        /// the NHibernate congiguration
        /// </summary>
        private static NHibernate.Cfg.Configuration cfg = null;

        #endregion
        #region constructors
        private Database( ) { cfg = new NHibernate.Cfg.Configuration(); }
        #endregion
        #region properties
        internal static Database Instance
        {
            get
            {
                if ( instance == null )
                    instance = new Database();
                return instance;
            }
        }
        #endregion

        /// <summary>
        /// gets the sessionfactory
        /// creates a new one if no one exists
        /// </summary>
        private ISessionFactory SessionFactory
        {
            get { return _sessionFactory ?? ( _sessionFactory = createSessionFactory() ); }
        }

        /// <summary>
        /// creates a new sessionfactory
        /// </summary>
        /// <returns></returns>
        private ISessionFactory createSessionFactory( )
        {
            return cfg.BuildSessionFactory();
        }
        /// <summary>
        /// opens a session
        /// </summary>
        /// <returns>session</returns>
        public ISession OpenSession( )
        {
            return SessionFactory.OpenSession();
        }

        /// <summary>
        /// updated the NHibernate confuguration
        /// </summary>
        private void updateConfuguration( )
        {

            try
            {
                cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = DatabaseConfiguration.Instance.GetConnectionString();
                x.GetType()
                    .GetMethod("Driver")
                    .MakeGenericMethod(DatabaseConfiguration.Instance.Driver)
                    .Invoke(x , null);
                x.GetType()
                    .GetMethod("Dialect")
                    .MakeGenericMethod(DatabaseConfiguration.Instance.Dialect)
                    .Invoke(x , null);
            });
                var serializer = new HbmSerializer() { Validate = true };

                using ( var stream = serializer.Serialize(DatabaseConfiguration.Instance.GetAssembly()) )
                {
                    cfg.AddInputStream(stream);
                }
            }
            catch ( DatabaseException  )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Updating the configuration failed!" ) );
            }

        }
      
        public void Update( object sender )
        {
            try
            {
                updateConfuguration();
            }
            catch ( DatabaseException  )
            {
                throw;
            }
           

        }
    }
}
