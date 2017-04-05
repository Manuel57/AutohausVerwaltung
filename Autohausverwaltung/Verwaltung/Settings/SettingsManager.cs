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
    /// <summary>
    /// class for managing settings
    /// </summary>
    public class SettingsManager
    {
        /// <summary>
        /// the settings filename
        /// </summary>
        private static string settingsFile = null;

        /// <summary>
        /// the instance
        /// </summary>
        private static volatile SettingsManager instance = null;

        /// <summary>
        /// the object for locking
        /// </summary>
        private static object sync = new object();
        /// <summary>
        /// the settings (später vlt collection)
        /// </summary>
        private DatabaseSettings settings = new DatabaseSettings();

        /// <summary>
        /// gets or sets the filename
        /// </summary>
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
        /// <summary>
        /// gets the instance
        /// </summary>
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

        /// <summary>
        /// shows a settingsdialog 
        /// </summary>
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

        /// <summary>
        /// saves the settings to a file
        /// </summary>
        private void Save( )
        {

            XmlSerializer ser = new XmlSerializer(typeof(DatabaseSettings));
            StreamWriter sw = new StreamWriter(SettingsFileName);
            ser.Serialize(sw , this.settings);
            sw.Close();

        }

        /// <summary>
        /// loads the settings from the file
        /// </summary>
        private void Load( )
        {
            if ( File.Exists(SettingsFileName) )
            {
                XmlSerializer ser = new XmlSerializer(( typeof(DatabaseSettings) ));
                StreamReader sr = new StreamReader(SettingsFileName);
                DatabaseSettings s = ser.Deserialize(sr) as DatabaseSettings;
                this.settings = s;
                sr.Close();
            }
        }

        /// <summary>
        /// Returns the Settings
        /// </summary>
        /// <returns></returns>
        public DatabaseSettings GetSettings()
        {
            return this.settings;
        }
    }
}
