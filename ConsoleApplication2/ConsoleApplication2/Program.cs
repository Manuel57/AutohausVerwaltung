using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager m = new Manager();
            m.OnMessage += (object o, SnowRemovalEventArgs e) =>
            {
                Console.WriteLine(e.SnowHeight); global::System.Console.WriteLine(e.RAbschnitt);
            };
            m.StartWork();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Main thread");
                System.Threading.Thread.Sleep(500);
            }
        }
    }
}
