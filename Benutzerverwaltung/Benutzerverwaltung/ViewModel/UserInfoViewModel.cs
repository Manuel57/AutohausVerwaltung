﻿// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-13</date>

using Benutzerverwaltung.Helpers;
using Benutzerverwaltung.Model;
using BenutzerverwaltungBL.Model.DataObjects;
using System;
using System.Collections.Specialized;

namespace Benutzerverwaltung.ViewModel
{
    public class UserInfoViewModel : ModelBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Action CloseAction { get; set; }
        public RelayCommand PrintCommand { get; set; }
        public RelayCommand OkCommand { get; set; }

        public UserInfoViewModel( Customer c)
        {
            
            this.OkCommand = new RelayCommand(( ) => { this.CloseAction(); });
            this.PrintCommand = new RelayCommand(( ) => { });
            this.Username = c.Username;
            this.Password = c.Password;
            this.OnPropertyChanged("Username");
            this.OnPropertyChanged("Password");
            this.OnPropertyChanged();
        }

    }
}
