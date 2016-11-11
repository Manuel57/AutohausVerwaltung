using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenutzerverwaltungBL.Model.BusinesObjects
{
    public class UserAuthenticationData
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public UserAuthenticationData( string username , string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}
