// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-13</date>

using BenutzerverwaltungBL.Controller;
using BenutzerverwaltungBL.Controller.Web;
using BenutzerverwaltungBL.Model.BusinesObjects;
using System;

namespace BenutzerverwaltungBL.Common
{
    public class UserdataGenerator
    {
        public static UserAuthenticationData CreateUserAuthentication( string name , DateTime bd , out string origPw )
        {
            UserAuthenticationData userdata = new UserAuthenticationData();
            try
            {
                string guidUsername = string.Format("{0}{1}{2}" , name?.Replace(" " , "") , bd.Year , new Random(bd.Day).Next(99));
                userdata.Username = "SmZ8mz9r54kgb7xc";
                userdata.Password = Guid.NewGuid().ToString().Replace("-" , "").Substring(26);
                origPw = userdata.Password;

                string jsonSend = JsonConverter.serializeData<UserAuthenticationData>(userdata);
                
                string jsonAnswer = HttpConnectionController.Instance.CreateUser(jsonSend);

                userdata = JsonConverter.deserializeData<UserAuthenticationData>(jsonAnswer);
                userdata.Username = guidUsername;
                //TOTO_ check username
                //userdata = new UserAuthenticationData
                //(
                //    guidUsername,
                //    MD5Hash.Compute(guidPassword)
                //);
            }
            catch ( Exception )
            {
                throw;
            }

            return userdata;
        }
    }
}
