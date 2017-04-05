// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-11</date>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Connection
{
    /// <summary>
    /// Class represeting a database user
    /// </summary>
    public class DbUser : IDbUser
    {
        public string Password
        {
            get;
        }

        public string Username
        {
            get;
        }
        public DbUser(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}
