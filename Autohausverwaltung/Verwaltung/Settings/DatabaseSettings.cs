using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verwaltung.Settings
{
    /// <summary>
    /// database settings
    /// </summary>
    [Serializable]
    public class DatabaseSettings : Settings
    {
        
        [CategoryAttribute("Connection"),
        DisplayName("Ip Address"),
        DescriptionAttribute("Which ip adress to use")]
        public IpAddress IpAddress
        {
            get;
            set;
        }

     
    }
}
