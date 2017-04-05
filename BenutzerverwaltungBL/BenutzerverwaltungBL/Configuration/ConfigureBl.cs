// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-13</date>

using Database;
using Database.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Verwaltung.Exception;
using Verwaltung.Settings;

namespace BenutzerverwaltungBL.Configuration
{
   public static class ConfigureBl
    {
        public static void Initialize()
        {

            try
            {
                DataAccessInitializing.Initialize(Assembly.GetExecutingAssembly());
                
            }
            catch (DatabaseException)
            {
                throw;
            }
        }
        public static void UpdateSettings(DatabaseSettings settings)
        {
            DataAccessInitializing.UpdateSettings(settings);
        }
    }
}
