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
    public class Database
    {

        private static ISessionFactory _sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if ( _sessionFactory == null )
                {
                    var cfg = new NHibernate.Cfg.Configuration();
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
                    _sessionFactory = cfg.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }
        public static ISession OpenSession( )
        {
            return SessionFactory.OpenSession();
        }
    }
}
