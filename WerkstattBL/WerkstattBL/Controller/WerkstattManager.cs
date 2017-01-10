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
        public static void GetMessagesForToday(DateTime now) { }
        public static void CreateReparatur(int repartID, int rechnungsnummer,DateTime date,string standort) { }
        public static void DeleteFromHelp(int rechnungsnummer, DateTime date, int kundenID) { }
        private static void DecreaseMenge(int repartID, string standort) { } 

    }
}
