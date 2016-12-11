using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Verwaltung.Dialogs;

namespace Verwaltung.Settings
{
    public class SettingsManager
    {
        private static string settingsFile = null;
        private static volatile SettingsManager instance = null;
        private static object sync = new object();
        private DatabaseSettings settings = new DatabaseSettings();
        public static string SettingsFileName
        {
            get
            {
                try
                {
                    if ( settingsFile == null )
                    {
                        settingsFile = string.Format(CultureInfo.InvariantCulture , "{0}.settings.xml" , Assembly.GetEntryAssembly().GetName().Name);
                    }

                    return settingsFile;
                }
                catch ( NullReferenceException )
                {
                    return "settings.xml";
                }
            }
            set
            {
                settingsFile = value;
            }
        }
        public static SettingsManager Instance
        {
            get
            {
                if ( instance == null )
                {

                    lock ( sync )
                    {
                        if ( instance == null )
                        {
                            instance = new SettingsManager();
                        }
                    }
                }
                return instance;
            }
        }
        public void ShowEditor( )
        {
            SettingsDialog dialog = new SettingsDialog()
            {
                Settings = this.settings

            };

            if ( dialog.ShowDialog() == true )
            {
                this.settings = dialog.Settings;
                this.Save();
            }
        }
        private SettingsManager( )
        {
            this.Load();
        }
        private void Save( )
        {

            XmlSerializer ser = new XmlSerializer(typeof(DatabaseSettings));
            StreamWriter sw = new StreamWriter(SettingsFileName);
            ser.Serialize(sw , this.settings);
            sw.Close();

        }
        private void Load( )
        {
            if ( File.Exists(SettingsFileName) )
            {
                XmlSerializer ser = new XmlSerializer(( typeof(DatabaseSettings) ));
                StreamReader sr = new StreamReader(SettingsFileName);
                DatabaseSettings s = ser.Deserialize(sr) as DatabaseSettings;
                this.settings = s;
                File.WriteAllText("log.txt" , this.settings.IpAddress.ToString());
                sr.Close();
            }
        }
    }
}
