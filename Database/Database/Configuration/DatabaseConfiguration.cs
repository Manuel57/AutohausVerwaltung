using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Configuration
{
    public class DatabaseConfiguration
    {
        public int MyProperty { get; set; }
        //
        //ConnectionString = "Provider=OraOLEDB.Oracle; Data Source=192.168.128.152/ora11g; User Id = d5a09; Password = d5a; OLEDB.NEW = True;";
        //        x.Driver<OleDbDriver>();
        //        x.Dialect<Oracle10gDialect>();
        internal void GetConnectionString()
        {

        }
    }
}
