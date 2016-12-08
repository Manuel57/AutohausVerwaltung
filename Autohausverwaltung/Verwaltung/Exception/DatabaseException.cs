using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verwaltung.Exception
{
    public class DatabaseException : System.Exception
    {

        private System.Exception exceptionThrown = null;
        private string customMessage = string.Empty;
        private object[] information = null;

        public System.Exception ExceptionThrown
        {

            private set
            {
                if ( value is System.Exception )
                {
                    exceptionThrown = value;
                }

            }
            get
            {
                return exceptionThrown;
            }
        }

        public string CustomMessage { set { customMessage = value; } get { return customMessage; } }

        public object[] Information
        {
            set
            {
                if ( value != null )
                {
                    information = value;
                }
            }
            get { return information; }
        }

        public DatabaseException( System.Exception exThrown , string customMessage , params object[] information )
        {
            this.ExceptionThrown = exThrown;
            this.CustomMessage = customMessage;
            this.Information = information;
        }
    }
}
