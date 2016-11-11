using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    internal class DatabaseException
    {
        private Exception exceptionThrown = null;
        private string customMessage = string.Empty;
        private object[] information = null;

        public Exception ExceptionThrown
        {
            set
            {
                if(value is Exception)
                {
                    exceptionThrown = value;
                }
                
            }
            get
            {
                return exceptionThrown;
            }
        }

        public string CustomMessage { get { return customMessage; } }

        public object[] Information { get { return information; } }
        
        public DatabaseException(Exception exThrown, string customMessage, params object[] information)
        {
            ExceptionThrown = exThrown;
            this.customMessage = customMessage;
            this.information = information;
        }
    }
}
