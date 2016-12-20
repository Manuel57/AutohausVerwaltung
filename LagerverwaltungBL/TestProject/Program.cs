using LagerverwaltungBL.Configuration;
using LagerverwaltungBL.Controller;
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
                IEnumerable<Werkstattlager> ret = TeileManager.GetAutoteileWerkstatt("Villach");
                foreach ( var item in ret )
                {
                    Console.WriteLine(item.Teil.Bezeichnung + " " 
                        + item.Teil.Preis + " " + item.Bestand);
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
