using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BenutzerverwaltungBL.Model.BusinesObjects
{
    [DataContract]
    public class UserAuthenticationData
    {
        [DataMember(Name ="username")]
        public string Username { get; set; }
        [DataMember(Name ="password")]
        public string Password { get;  set; }
        public UserAuthenticationData( ) { }
        public UserAuthenticationData( string username , string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}
