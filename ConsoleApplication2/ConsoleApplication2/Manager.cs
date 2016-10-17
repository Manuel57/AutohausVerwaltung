using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public class Manager
    {
        public event EventHandler<SnowRemovalEventArgs> OnMessage;
        private Thread thread;

        public Manager()
        {
            this.thread = new Thread(blablabla);

        }
        public void StartWork()
        {
            thread.Start();
        }


        private void blablabla()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(TimeSpan.FromSeconds(1));
                OnMessage("I got it",new SnowRemovalEventArgs() { SnowHeight = 12, RAbschnitt = "A1" } );
            }
        }
    }
}
