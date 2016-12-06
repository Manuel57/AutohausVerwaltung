using BenutzerverwaltungBL.Configuration;
using BenutzerverwaltungBL.Controller;
using BenutzerverwaltungBL.Model;
using BenutzerverwaltungBL.Model.DataObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
   public class Program
    {
        private static string testpath = @"C:\Users\Thomas Huber\Documents\Schule\5.Klasse\BSD\ERD.pdf";
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            List<Customer> allCustomer = new List<Customer>();
            try
            {
                ConfigureBl.Initialize();
                foreach(Customer c in CustomerManager.GetAllCustomers())
                {
                    allCustomer.Add(c);
                }
                Customer manuel = allCustomer.Find(item => item.CustomerId == 1);
                Reparatur rep = new Reparatur();
                

                 RechnungManager.InsertRechnungAsDoc(manuel.Rechnungen.ElementAt(1));
                //List<byte[]> docs = RechnungManager.GetAllRechnungenForKunde(manuel.CustomerId);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void displayList(List<Customer> lisst)
        {
            foreach(Customer c in lisst)
            {

                Console.WriteLine(c.FirstName + "\nRechnungen" + ((c.Rechnungen.ElementAt(1) as Rechnung).Reparaturen.ElementAt(1) as Reparatur).RepArt.ToString() + "\n");
                Console.WriteLine();
            }
        }

       
    }
}
