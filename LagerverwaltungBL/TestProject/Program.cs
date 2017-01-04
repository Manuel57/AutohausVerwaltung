using Database.Common;
using Database.Common.Impl;
using LagerverwaltungBL.Configuration;
using LagerverwaltungBL.Controller;
using LagerverwaltungBL.Model;

using System;
using System.Collections;
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

                List<Zentrallager> lager = SdoManager.GetZentrallager();
                //CongifManager.UpdateSettings(new DatabaseSettings() { IpAddress = IpAddress.Extern });
                //IEnumerable<Autoteile> ret = TeileManager.GetAutoteileWerkstatt("Villach");
                //foreach ( var item in ret )
                //{
                //    Console.WriteLine(item.Bezeichnung);
                //}

                //Console.WriteLine(TeileManager.GetBestand("Villach", "Blinker"));

                //HashSet<Column> columns = new HashSet<Column>();
                //columns.Add(new Column() { Name = "Standort" , Type = NHibernateUtil.String });
                //columns.Add(new Column() { Name = "Name" , Type = NHibernateUtil.String });
                //columns.Add(new Column() { Name = "t.X" , Alias = "lon" , Type = NHibernateUtil.String });

                //using ( IRepository rep = RepositoryFactory.Instance.CreateRepository<RepositoryForSpecialDataTypes>() )
                //{
                //    Console.WriteLine("OK");
                //    IList lst = ( rep as RepositoryForSpecialDataTypes ).GetQuery("zentrallager, table(sdo_util.getvertices(koordinatenz)) t" ,
                //        "not koordinatenz is null" , columns);
                //    foreach ( object[] item in lst )
                //    {
                //        foreach ( var itm in item )
                //        {
                //            Console.Write("ITEM: ");
                //            Console.WriteLine(itm);

                //        }
                //    }
                //}
                foreach ( var item in lager )
                {
                    Console.WriteLine(item.Coordinates?.X);
                }
                Console.WriteLine("Finished");


            }
            catch ( Exception e )
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
