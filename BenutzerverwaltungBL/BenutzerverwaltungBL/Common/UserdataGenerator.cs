// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-13</date>

using BenutzerverwaltungBL.Model.BusinesObjects;
using System;

namespace BenutzerverwaltungBL.Common
{
    public  class UserdataGenerator
    {
        public static UserAuthenticationData CreateUserAuthentication(string name, DateTime bd)
        {
            UserAuthenticationData userdata = null;
            try
            {
                string guidUsername = string.Format("{0}{1}{2}" , name?.Replace(" " , "") , bd.Year,new Random(bd.Day).Next(99));

                //TOTO_ check username
                string guidPassword = Guid.NewGuid().ToString().Replace("-" , "");

                userdata = new UserAuthenticationData
                (
                    guidUsername,
                    MD5Hash.Compute(guidPassword)
                );
            }
            catch ( Exception  )
            {
                throw;
            }

            return userdata;
        }
    }
}
