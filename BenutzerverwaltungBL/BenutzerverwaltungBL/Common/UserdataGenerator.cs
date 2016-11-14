// <copyright file="BenutzerverwaltungBL.Common.userdatagenerator.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-13</date>
// </copyright>

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
