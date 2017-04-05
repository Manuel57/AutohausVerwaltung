// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-12-6</date>

using Benutzerverwaltung.Controls;
using BenutzerverwaltungBL.Controller;
using BenutzerverwaltungBL.Model.DataObjects;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using Verwaltung.Exception;

namespace Benutzerverwaltung.View
{
    /// <summary>
    /// Interaction logic for ReparaturenView.xaml
    /// </summary>
    public partial class ReparaturenView : Window
    {
        public ReparaturenView( )
        {
            InitializeComponent();
        }
        public ReparaturenView( string customerId ) : this()
        {
            try
            {
                this.listView.ItemsSource = CustomerManager.GetSingleCustomerById(int.Parse(customerId)).Rechnungen;
            }
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }

        }

        /// <summary>
        /// Creates a  <see cref="Rechnung"/> as pdf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateRechnung( object sender , RoutedEventArgs e )
        {
            try
            {
                int rechnungsID = (sender as RechnungAusstellenButton).RechnungsNummer;
                int customerId = (sender as RechnungAusstellenButton).CustomerId;
              
                RechnungManager.InsertRechnungAsDoc(rechnungsID);
                this.listView.ItemsSource = null;
                this.listView.ItemsSource = CustomerManager.GetSingleCustomerById(customerId).Rechnungen;

                File.WriteAllBytes("Rechnung-" + rechnungsID + ".pdf" , RechnungManager.GetCertainRechnungForKunde(customerId,rechnungsID));
                Process.Start("Rechnung-" + rechnungsID+ ".pdf");
            }
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }
        }
    }
}
