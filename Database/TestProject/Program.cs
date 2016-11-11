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

namespace TestProject
{

    class Program
    {
        static void Main( string[] args )
        {
            DatabaseConfiguration.Instance.registerAll(
                DefaultConfig.OLEDB_PROVIDER ,
                "212.152.179.117" , "ora11g" , new DbUser("d5a09" , "d5a") ,
                DefaultConfig.ORACLE_DIALECT , DefaultConfig.ORACLE_DRIVER ,
                Assembly.GetExecutingAssembly());
            IRepository repository = RepositoryFactory.Instance.
                                    CreateRepository<Repository>();

            Employee emp = repository.GetById<Employee>(1);
            Console.WriteLine(emp.Mnr + " " + emp.Name);
        }
    }
}
