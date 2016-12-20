using Database.Common;
using Database.Common.Impl;
using LagerverwaltungBL.Configuration;
using LagerverwaltungBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verwaltung.Settings;

namespace TestProject
{
    class Program
    {
        static void Main( string[] args )
        {
            try
            {
                CongifManager.Initialize();
                IEnumerable<Autoteile> ret = null;
                using ( IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>() )
                {
                    var x = repository.GetById<Autoteile>("Blinker");
                    //foreach (var u in ret)
                    //{
                    //    foreach ( var item in u.Werkstattlager )
                    //    {
                    //        Console.WriteLine(item);
                    //    }
                    Console.WriteLine(x);
                }

            }
            catch ( Exception e )
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
