using BenutzerverwaltungBL.Model.BusinesObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenutzerverwaltungBL.Common
{
    public class UserdataGenerator
    {
        public static UserAuthenticationData CreateUserAuthentication( )
        {
            UserAuthenticationData userdata = null;
            try
            {
                string guidUsername = Guid.NewGuid().ToString().Replace("-" , "");
                string guidPassword = Guid.NewGuid().ToString().Replace("-" , "");

                userdata = new UserAuthenticationData
                (
                    guidUsername,
                    MD5Hash.Compute(guidPassword)
                );
            }
            catch ( Exception e )
            {
                throw;
            }

            return userdata;
        }
    }
}
