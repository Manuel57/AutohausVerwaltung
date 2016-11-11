using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace Database
{
    public class ExceptionHelper
    {
        public static void HandleException(Exception e, IInternalLogger logger,
            string message, params string[] additionalInformation)
        {
            //logger.Error("An error has occoured");
            //logger.Error(e.Message , e);
            //logger.Info("Additional information below");
            //Array.ForEach<string>(additionalInformation , item => logger.Info(item));
        }
    }
}
