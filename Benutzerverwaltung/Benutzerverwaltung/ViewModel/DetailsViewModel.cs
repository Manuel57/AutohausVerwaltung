// <copyright file="Benutzerverwaltung.ViewModel.DetailsViewModel.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-12-6</date>
// </copyright>

using Benutzerverwaltung.Helpers;
using Benutzerverwaltung.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benutzerverwaltung.ViewModel
{
    public class DetailsViewModel : Model.ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Address { get; set; }
        public string CustomerId { get; set; }
        public string Username { get; set; }
        public RelayCommand ReparaturenCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand ChangeCommand { get; set; }

        public DetailsViewModel( )
        {
            this.ReparaturenCommand = new RelayCommand(this.ShowReparaturen);
            this.DeleteCommand = new RelayCommand(this.delete);
            this.ChangeCommand = new RelayCommand(this.edit);
        }
        public void ChangedAll( )
        {
            this.OnPropertyChanged("FirstName");
            this.OnPropertyChanged("LastName");
            this.OnPropertyChanged("BirthDate");
            this.OnPropertyChanged("Address");
            this.OnPropertyChanged("CustomerId");
            this.OnPropertyChanged("Username");
            this.OnPropertyChanged();
        }

        private void ShowReparaturen( )
        {
            ReparaturenView rv = new ReparaturenView(this.CustomerId);
            rv.Show();
        }

        private void edit( )
        {

        }
        private void delete( )
        {

        }
    }
}
