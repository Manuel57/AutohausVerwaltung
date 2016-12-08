// <copyright file="Benutzerverwaltung.ViewModel.DetailsViewModel.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-12-6</date>
// </copyright>

using Benutzerverwaltung.Helpers;
using Benutzerverwaltung.View;
using BenutzerverwaltungBL.Controller;
using BenutzerverwaltungBL.Model.DataObjects;
using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Benutzerverwaltung.ViewModel
{
    public class DetailsViewModel : Model.ModelBase
    {
        public Customer Kunde { get; set; }
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
            this.OnPropertyChanged("Kunde");
            this.OnPropertyChanged();
        }

        private void ShowReparaturen( )
        {

            try
            {
                ReparaturenView rv = new ReparaturenView(this.Kunde.CustomerId.ToString());
                rv.Show();
            }
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }

        }

        private void edit( )
        {

            try
            {
                CustomerManager.UpdateCustomer(this.Kunde);
            }
           
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }

        }
        private void delete( )
        {

            try
            {
                MessageBoxResult dialogResult = MessageBox.Show(Properties.Resources.AreYouSure , "Delete" , MessageBoxButton.YesNo , MessageBoxImage.Warning);
                if ( dialogResult.Equals(MessageBoxResult.Yes) )
                {
                    this.Kunde.Rechnungen.Where<Rechnung>(item =>
                    !item.IsAlreadyPdf).ToList<Rechnung>().
                    ForEach(item => RechnungManager.InsertRechnungAsDoc(item));
                    CustomerManager.DeleteCustomer(this.Kunde);
                }
            }
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }

        }
    }
}
