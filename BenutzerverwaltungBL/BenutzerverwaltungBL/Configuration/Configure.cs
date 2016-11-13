using Database.Configuration;
using Database.Connection;
using Database.Connection.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BenutzerverwaltungBL.Configuration
{
    public static class Configure
    {
        public static bool ConfigureDatabaseUseDefaults(string Ipadress,string username, string password)
        {

            try
            {
                DatabaseConfiguration.Instance.RegisterAll(
                DefaultConfig.OLEDB_PROVIDER,
                IPAddress.Parse(Ipadress), "ora11g", new DbUser(username, password),
                DefaultConfig.ORACLE_DIALECT, DefaultConfig.ORACLE_DRIVER,
                Assembly.GetExecutingAssembly());
                return true;
            }
           
            catch (Exception ex)
            {
                throw ;
            }

        }

        public static bool ConfigureDatabase(string provider, string adress,string service, string username,
                                            string password,string dialect,string driver,Assembly assembly)
        {
            throw (new NotImplementedException());
        }
    }
}
