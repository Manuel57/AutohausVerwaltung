using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDM.Win.Events
{
    public class CallBackEventArgs<T> : EventArgs
    {
        public T CallBack { get; private set; }

        public CallBackEventArgs(T callback)
        {
            this.CallBack = callback;
        }
    }
}
