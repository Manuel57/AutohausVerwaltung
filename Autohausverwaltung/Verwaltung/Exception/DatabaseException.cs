using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verwaltung.Exception
{
    /// <summary>
    /// Custom exception class for database exceptions
    /// </summary>
    public class DatabaseException : System.Exception
    {
        /// <summary>
        /// the original exception
        /// </summary>
        private System.Exception exceptionThrown = null;
        /// <summary>
        /// a custom messsage
        /// </summary>
        private string customMessage = string.Empty;
        /// <summary>
        /// additional information
        /// </summary>
        private object[] information = null;

        /// <summary>
        /// gets or sets the original exception
        /// </summary>
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

        /// <summary>
        /// gets or sets the custom message
        /// </summary>
        public string CustomMessage
        {
            set { customMessage = value; }
            get { return customMessage; }
        }

        /// <summary>
        /// gets or sets the information
        /// </summary>
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
