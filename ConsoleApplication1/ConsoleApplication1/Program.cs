using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("log.txt");
            StreamWriter sw = new StreamWriter("create.txt");
            string line = sr.ReadLine();
            string cTab = "CREATE TABLE ";
            Console.WriteLine("i: INTEGERE\tv: VARCHAR\td: DATE\totherwise input");
            Console.WriteLine("PRIMATY KEY Attubutes");
            while (line != null)
            {
                sw.Write(cTab);
                string tabName = line.Substring(0, line.IndexOf("(") + 1);
                line = line.Substring(tabName.Length);
                line = line.Split(')')[0];
                string[] vals = line.Split(',');
                sw.WriteLine(tabName);
                for (int i = 0; i < vals.Length; i++)
                {
                    Console.WriteLine(vals[i]);
                    string input = Console.ReadLine();
                    string type = "";
                    switch (input)
                    {
                        case "d":
                            type = "DATE";
                            break;
                        case "i": type = "INTEGER"; break;
                        case "v": type = "VARCHAR"; break;
                        default:
                            type = input;
                            break;
                    }
                    sw.WriteLine(vals[i] + "     " + type+",");
                }
                Console.Write("Primary Key: " );
                string pk = Console.ReadLine();
                sw.WriteLine("PRIMARY KEY " + pk);
                sw.WriteLine(");");
                sw.WriteLine();
                line = sr.ReadLine();

            }

            sw.Close();
            sr.Close();
        }
    }
}
