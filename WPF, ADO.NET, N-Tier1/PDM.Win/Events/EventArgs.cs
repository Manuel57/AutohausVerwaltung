using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDM.Win.Events
{

    public class VisibleEventArgs : EventArgs
    {

    }
    public class AddEventArgs : EventArgs
    {

    }

    public class SearchEventArgs : EventArgs
    {

    }

    public class RefreshEventArgs : EventArgs
    {

    }
    public class CallBackEventHander
    {
        static public event EventHandler CustomEvent;

        static public void RaiseMyCustomEvent(object sender, EventArgs args)
        {
            if (CustomEvent != null) CustomEvent(sender, args);
        }
    }
}
