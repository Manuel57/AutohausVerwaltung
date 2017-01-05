// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-12-6</date>

using Benutzerverwaltung.Helpers;
using Benutzerverwaltung.View;
using BenutzerverwaltungBL.Controller;
using BenutzerverwaltungBL.Model.DataObjects;
using System;
using System.Linq;
using System.Windows;
using Verwaltung.Exception;

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

        /// <summary>
        /// Shows the Reparaturen view
        /// </summary>
        private void ShowReparaturen( )
        {
            try
            {
                ReparaturenView rv = new ReparaturenView(
                    this.Kunde.CustomerId.ToString());
                rv.Show();
            }
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }
        }

        /// <summary>
        /// Updates the customer
        /// </summary>
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

        /// <summary>
        /// Removes the customer
        /// Saves all the customer's bills before deleting
        /// </summary>
        private void delete( )
        {

            try
            {
                MessageBoxResult dialogResult = MessageBox.Show(Properties.Resources.AreYouSure , "Delete" , MessageBoxButton.YesNo , MessageBoxImage.Warning);
                if ( dialogResult.Equals(MessageBoxResult.Yes) )
                {
                    this.Kunde.Rechnungen.Where<Rechnung>(item =>
                        !item.IsAlreadyPdf).ToList<Rechnung>().
                        ForEach(item => RechnungManager.InsertRechnungAsDoc(item.Rechnungsnummer));
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
