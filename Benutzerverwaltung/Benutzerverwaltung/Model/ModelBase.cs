// <copyright file="Benutzerverwaltung.ModelBase.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-12</date>
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Benutzerverwaltung.Model
{
    public class ModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// OnPropertyChanged
        /// </summary>
        /// <param name="propName">Name of the changed property</param>
        protected virtual void OnPropertyChanged( [CallerMemberName]String propName = "" )
        {
            if ( this.PropertyChanged != null )
            {
                this.PropertyChanged(this , new PropertyChangedEventArgs(propName));
            }
        }
    }
}
