using Database.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerkstattBL.Controller
{
   public static class WerkstattManager
    {
        private static IRepository repository = null;

        public static void GetMessagesForToday(DateTime now) { }
        public static void CreateReparatur(int RepartID, int Rechnungsnummer,DateTime date,string Standort) { }
        private static void DecreaseMenge(int RepartID, string Standort) { } 
    }
}
