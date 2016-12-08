// <copyright file="Benutzerverwaltung.Helpers.ExceptionHelper.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-12-7</date>
// </copyright>

using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Benutzerverwaltung.Helpers
{
    public class ExceptionHelper
    {
       

        public static void Handle(Exception e)
        {
            string msg = string.Empty;
            StringBuilder sb = new StringBuilder();
            if ( e is DatabaseException )
            {
                sb.AppendLine((e as DatabaseException).CustomMessage);
                sb.AppendLine("Additional information:");
                ( e as DatabaseException ).Information.ToList<object>().ForEach(i => sb.AppendLine(i.ToString()));
            }
            sb.AppendLine(e.Message);
            sb.AppendLine(e.InnerException?.Message);

            MessageBox.Show(sb.ToString() , "Error" , MessageBoxButton.OK , MessageBoxImage.Error,MessageBoxResult.None);
            
        }
        
    }
}
