// <copyright file="BenutzerverwaltungBL.Common.userdatagenerator.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-13</date>
// </copyright>

using BenutzerverwaltungBL.Model.BusinesObjects;
using BenutzerverwaltungBL.Model.DataObjects;
using Database.Common;
using Database.Common.Impl;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
