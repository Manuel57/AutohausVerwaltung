using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Verwaltung.Exception
{
    /// <summary>
    /// class responsible for handling exceptions
    /// </summary>
    public sealed class ExceptionHelper
    {
        /// <summary>
        /// creates a message string representing the given exception
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string CreateMessage( System.Exception e )
        {
            string msg = string.Empty;
            StringBuilder sb = new StringBuilder();
            if ( e is DatabaseException )
            {
                sb.AppendLine(( e as DatabaseException ).CustomMessage);
                sb.AppendLine("Additional information:");
                ( e as DatabaseException ).Information.ToList<object>().ForEach(i => sb.AppendLine(i.ToString()));
            }
            sb.AppendLine(e.Message);
            sb.AppendLine(e.InnerException?.Message);

            return sb.ToString();
        }
        /// <summary>
        /// Handles the exception
        /// </summary>
        /// <param name="e"></param>
        public static void Handle( System.Exception e )
        {
                MessageBox.Show(CreateMessage(e) , "Error" , MessageBoxButton.OK , MessageBoxImage.Error , MessageBoxResult.None);
        }
    }
}
