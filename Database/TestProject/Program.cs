using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Database.Configuration;
using Database.Connection.Defaults;
using Database.Connection;
using System.Reflection;
using Database.Common.Impl;
using Database.Common;
using System.Net;

namespace TestProject
{

    // <copyright file="">
    // Copyright (c) 2016 All Rights Reserved
    // <author>Manuel Lackenbucher</author>
    // <author>Thomas Huber</author>
    // <date></date>
    // </copyright>

    class Program
    {
        static void Main( string[] args )
        {
            DatabaseConfiguration.Instance.registerAll(
                DefaultConfig.OLEDB_PROVIDER ,
                IPAddress.Parse("212.152.179.117") , "ora11g" , new DbUser("d5a09" , "d5a") ,
                DefaultConfig.ORACLE_DIALECT , DefaultConfig.ORACLE_DRIVER ,
                Assembly.GetExecutingAssembly());

            IRepository repository = RepositoryFactory.Instance.
                                    CreateRepository<Repository>();

            Employee emp = repository.GetById<Employee>(1);
            Console.WriteLine(emp.Mnr + " " + emp.Name);
        }
    }
}
