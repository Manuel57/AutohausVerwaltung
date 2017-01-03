using LagerVerwaltung.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerVerwaltung.Helpers
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public class JsCommunication
    {
        private Action<string> jsFinishedCallback;

   

        public JsCommunication( Action<string> jsFinishedCallback )
        {
            this.jsFinishedCallback = jsFinishedCallback;
        }

        public void FinishedCalculating( string message )
        {
            jsFinishedCallback(message);
        }
    }
}
