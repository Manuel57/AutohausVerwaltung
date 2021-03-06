﻿using Database.Configuration;
using Database.Connection;
using Database.Connection.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Verwaltung.Exception;
using Verwaltung.Settings;

namespace Database.Common
{
    public static class DataAccessInitializing
    {
        /// <summary>
        /// Method for initializing setting for the Database connection
        /// </summary>
        /// <param name="Ipadress">The IP address</param>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns></returns>
        public static bool Initialize( string Ipadress , string username , string password )
        {
            try
            {
                DatabaseConfiguration.Instance.RegisterAll(
                DefaultConfig.OLEDB_PROVIDER ,
                IPAddress.Parse(Ipadress) , "ora11g" , new DbUser(username , password) ,
                DefaultConfig.ORACLE_DIALECT , DefaultConfig.ORACLE_DRIVER ,
                Assembly.GetExecutingAssembly());

                return true;
            }
            catch ( DatabaseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Initializing failed!") );
            }



        }

        public static bool Initialize( Assembly assembbly )
        {
            try
            {
                DatabaseConfiguration.Instance.RegisterAll(
                DefaultConfig.OLEDB_PROVIDER ,
                IPAddress.Parse(Properties.Settings.Default.IpCurrent) , Properties.Settings.Default.ServiceName ,
                new DbUser(Properties.Settings.Default.User , Properties.Settings.Default.Password) ,
                DefaultConfig.ORACLE_DIALECT , DefaultConfig.ORACLE_DRIVER ,
              assembbly);


                return true;
            }
            catch ( DatabaseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Initializing failed!") );
            }

        }

        public static void UpdateSettings( DatabaseSettings settings )
        {
            switch ( settings.IpAddress )
            {
                case IpAddress.Extern:
                    Properties.Settings.Default.IpCurrent = Properties.Settings.Default.IpAdresseExtern;
                    break;
                case IpAddress.Intern:
                    Properties.Settings.Default.IpCurrent = Properties.Settings.Default.IpAdresseIntern;
                    break;
            }
            Properties.Settings.Default.Save();
        }
    }
}
