﻿// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-11</date>

using Database.Common.ObserverPattern;
using Database.Configuration;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.Attributes;
using NHibernate.Metadata;
using NHibernate.Persister.Entity;
using System;
using System.Collections.Generic;
using Verwaltung.Exception;

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

        public static string GetTableName<T>( )
        {
            IClassMetadata md = Connection.Database.Instance.SessionFactory.GetClassMetadata(typeof(T));
            AbstractEntityPersister aep = md as AbstractEntityPersister;
            return aep.TableName;
            //return new NHibernate.Cfg.Configuration().GetClassMapping(typeof(T)).RootTable.Name;
        }

        public static string GetColumnName<T>( string property )
        {
            IClassMetadata md = Connection.Database.Instance.SessionFactory.GetClassMetadata(typeof(T));
            return ( md as AbstractEntityPersister ).GetPropertyColumnNames(property)?[0];
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
                x.ConnectionString = DatabaseConfiguration.Instance
                 .GetConnectionString();
                x.Driver<OleDbDriver>();
                x.Dialect<Oracle10gDialect>();

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

                using ( var stream = serializer.Serialize(DatabaseConfiguration.
                    Instance.GetAssembly()) )
                {
                    cfg.AddInputStream(stream);
                }
            }
            catch ( DatabaseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Updating the configuration failed!") );
            }

        }

        public void Update( object sender )
        {
            try
            {
                updateConfuguration();
            }
            catch ( DatabaseException )
            {
                throw;
            }


        }
    }
}
