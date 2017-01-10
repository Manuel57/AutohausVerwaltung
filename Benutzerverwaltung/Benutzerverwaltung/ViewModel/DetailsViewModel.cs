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
        public Action Close { get; internal set; }

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
                if ( !string.IsNullOrEmpty(this.Kunde.Adress) &&
                    !string.IsNullOrEmpty(this.Kunde.FirstName) &&
                    !string.IsNullOrEmpty(this.Kunde.LastName) &&
                    !string.IsNullOrEmpty(this.Kunde.Username) &&
                    !string.IsNullOrEmpty(this.Kunde.WerkstattKonzern) )
                    CustomerManager.UpdateCustomer(this.Kunde);
                else
                    throw new Exception("Nicht alle Werte eingegeben!");
            }

            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }
        }

        /// <summary>
        /// Removes the customer
        /// </summary>
        private void delete( )
        {

            try
            {
                MessageBoxResult dialogResult = MessageBox.Show(Properties.Resources.AreYouSure , "Delete" , MessageBoxButton.YesNo , MessageBoxImage.Warning);
                if ( dialogResult.Equals(MessageBoxResult.Yes) )
                {
                    CustomerManager.DeleteCustomer(this.Kunde);
                    this.Close();
                }
            }
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }
        }
    }
}
