﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LagerVerwaltung.Helpers;
using LagerVerwaltung.ViewModel;
using Verwaltung.Exception;
using LagerverwaltungBL.Model;
using System.IO;
using LagerverwaltungBL.Controller;
using System.Windows.Navigation;
using System.Threading;
using System.Security.Permissions;

namespace LagerVerwaltung.View
{

    /// <summary>
    /// Interaktionslogik für BestellenView.xaml
    /// </summary>
    public partial class BestellenView : Window
    {
        //auf der karte ghörn dann de Standorte der Lager eingezeichnet
        private Autoteile autoteil = new Autoteile() { Bezeichnung ="default",Preis=0};
        private string Werkstatt = default(string);
        private Uri browserUri = new Uri("https://www.google.com/maps/@46.953771,14.0898729,9.25z", UriKind.Absolute);

        private BestellenView( )
        {
            InitializeComponent();
            this.browser.LoadCompleted += BrowserLoadCompleted;
        }

        private void BrowserLoadCompleted( object sender , NavigationEventArgs e )
        {
            //this.browser.InvokeScript("initialize");
            new Thread(( ) =>
            {
                System.Threading.Thread.Sleep(1000);
                this.Dispatcher.Invoke(( ) =>
                {
                    this.browser.InvokeScript("initMap");
                    
                });
            }).Start();
            
        }
        public void FinishedCalculating(string msg )
        {
            this.txtTime.Text = msg;
        }

        public BestellenView( Autoteile autoteile ,string Werkstatt) : this()
        {


            this.autoteil = autoteile;
            this.Werkstatt = Werkstatt;
            try
            {
                File.WriteAllText("./../../ScriptAndPages/data.js" , SdoManager.GetJsonCoordinates(autoteil , "Villach"));
                string path = System.IO.Path.GetFullPath("./../../ScriptAndPages/Map.html");

                this.browser.Navigate(new Uri(path , UriKind.Absolute));
            }
            catch ( Exception e )
            {
                ExceptionHelper.Handle(e);
            }
        }

        private void JsFinishedCallback(string msg)
        {
            this.txtTime.Text = msg;
        }

        private void Window_Initialized( object sender , EventArgs e )
        {
            try
            {

                (this.root.DataContext as BestellenViewModel).init(this.Werkstatt, this.autoteil);
                ( this.root.DataContext as BestellenViewModel ).TeilChanged();
                this.browser.ObjectForScripting = new JsCommunication(JsFinishedCallback);

            }

            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }

        }

        private void ShowGeomap( object sender , RoutedEventArgs e )
        {
            new Geomap().Show();
        }
    }
}
