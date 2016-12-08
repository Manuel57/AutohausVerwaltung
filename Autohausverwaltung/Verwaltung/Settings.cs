using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Verwaltung.Settings
{
    public class Settings
    {
        public void Save()
        {
            XmlSerializer ser = new XmlSerializer(this.GetType());
            StreamWriter sw = new StreamWriter("settings.xml");
            ser.Serialize(sw , this);
            sw.Close();
        }
        public void Load( )
        {
            XmlSerializer ser = new XmlSerializer(this.GetType());
            StreamReader sr = new StreamReader("settings.xml");
            Settings s = ser.Deserialize(sr) as Settings;
            sr.Close();
            this.Build(s);
        }
        public virtual void Build(Settings setting )
        {
            
        }
    }
}
