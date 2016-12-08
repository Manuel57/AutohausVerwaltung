using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verwaltung.Settings
{
    public class DatabaseSettings : Settings
    {
        public enum Ip { Intern, Extern }


        [CategoryAttribute("Connection"),
        DisplayName("Ip Address"),
        DescriptionAttribute("Which ip adress to use")]
        public Ip IpAddress
        {
            get;
            set;
        }

        public override void Build( Settings setting )
        {
            base.Build(setting);
            DatabaseSettings dbs = setting as DatabaseSettings;
            this.IpAddress = dbs.IpAddress;
        }
    }
}
