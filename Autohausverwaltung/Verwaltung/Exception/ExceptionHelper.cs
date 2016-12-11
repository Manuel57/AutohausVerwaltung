using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Verwaltung.Exception
{
    public sealed class ExceptionHelper
    {
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
        public static void Handle( System.Exception e )
        {
            lock ( typeof(ExceptionHelper) )
            {
                MessageBox.Show(CreateMessage(e) , "Error" , MessageBoxButton.OK , MessageBoxImage.Error , MessageBoxResult.None);
            }
        }
    }
}
