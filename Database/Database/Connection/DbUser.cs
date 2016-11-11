using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Connection
{
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
