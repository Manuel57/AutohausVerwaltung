// <copyright file="Benutzerverwaltung.Helpers.RelayCommand.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-12</date>
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Benutzerverwaltung.Helpers
{
    public class RelayCommand : ICommand
    {
        #region Fields

        readonly Action _execute;
        readonly Predicate<object> _canExecute;

        #endregion // Fields

        #region Constructors


        public RelayCommand( Action execute )
            : this(execute , null)
        {
        }


        public RelayCommand( Action execute , Predicate<object> canExecute )
        {
            if ( execute == null )
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion // Constructors

        #region ICommand Members

        public bool CanExecute( object parameter )
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute( )
        {
            _execute();
        }
        public void Execute( object param )
        {
            Execute();
        }

        #endregion // ICommand Members
    }
}
