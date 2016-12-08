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
using Verwaltung.Exception;

namespace Benutzerverwaltung.Helpers
{
    public class ExceptionManager : Verwaltung.Exception.ExceptionHelper
    {
        private static ExceptionManager instance;

        public static ExceptionManager Instance
        {
            get
            {
                if ( instance == null )
                    instance = new ExceptionManager();
                return instance;
            }
        }
        private ExceptionManager( ) { }

        public override void Handle( Exception e )
        {
            MessageBox.Show(this.CreateMessage(e) , "Error" , MessageBoxButton.OK , MessageBoxImage.Error , MessageBoxResult.None);

        }


    }
}
