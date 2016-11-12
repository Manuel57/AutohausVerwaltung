// <copyright file="Database.databaseexception.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-11</date>
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    /// <summary>
    /// DatabaseExcption class 
    /// own excetpion to throw
    /// </summary>
    
    internal class DatabaseException : Exception
    {
        
        private Exception exceptionThrown = null;
        private string customMessage = string.Empty;
        private object[] information = null;

        public Exception ExceptionThrown
        {
            
            private set
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
            this.ExceptionThrown = exThrown;
            this.customMessage = customMessage;
            this.information = information;
        }
    }
}
