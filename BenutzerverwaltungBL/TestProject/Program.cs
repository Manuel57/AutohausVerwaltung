using BenutzerverwaltungBL.Configuration;
using BenutzerverwaltungBL.Controller;
using BenutzerverwaltungBL.Model.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
   public class Program
    {
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
                displayList(allCustomer);
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

                Console.WriteLine(c.FullName + "\nRechnungen" + ((c.Rechnungen.ElementAt(1) as Rechnung).Reparaturen.ElementAt(1) as Reparatur).RepArt.ToString() + "\n");
                Console.WriteLine();
            }
        }
    }
}
