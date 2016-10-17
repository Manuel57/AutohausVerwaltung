using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public class SnowRemovalEventArgs : EventArgs
    {
        public int SnowHeight { get; set; }
        public string RAbschnitt { get; set; }
    }
}
