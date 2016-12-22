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
        /// Create Rechnung  !!!!Verbesserungsbedürftig 
        ///  -> BL benötigt funktion für rechnung  erstellen per Rechnungsnummer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateRechnung( object sender , RoutedEventArgs e )
        {
            try
            {
                Rechnung r = CustomerManager.GetSingleCustomerById(( sender as RechnungAusstellenButton ).CustomerId).Rechnungen.ToList<Rechnung>().Find(item => item.Rechnungsnummer.Equals(( sender as RechnungAusstellenButton ).RechnungsNummer));
                RechnungManager.InsertRechnungAsDoc(r);
                this.listView.ItemsSource = null;
                this.listView.ItemsSource = CustomerManager.GetSingleCustomerById(r.Kunde.CustomerId).Rechnungen;

                File.WriteAllBytes("Rechnung-" + r.Rechnungsnummer + ".pdf" , RechnungManager.GetCertainRechnungForKunde(r.Kunde.CustomerId , r.Rechnungsdatum));
                Process.Start("Rechnung-" + r.Rechnungsnummer + ".pdf");
            }
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }
        }
    }
}
