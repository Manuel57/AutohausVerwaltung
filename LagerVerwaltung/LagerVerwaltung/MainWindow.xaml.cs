﻿using LagerVerwaltung.Helpers;
using LagerVerwaltung.View;
using LagerVerwaltung.ViewModel;
using LagerverwaltungBL.Configuration;
using LagerverwaltungBL.Controller;
using LagerverwaltungBL.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Verwaltung.Exception;
using Verwaltung.Settings;

namespace LagerVerwaltung
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        public MainWindow( )
        {
            InitializeComponent();
            //List<Test> coll = new List<Test>() { new Test() , new Test() };
            //JsonArrayAttribute arr = new JsonArrayAttribute();
            //string s = string.Format("var coordinates ={0};", JsonConvert.SerializeObject(coll, Formatting.Indented));

            //MessageBox.Show(s);
        }

        private void lvTeile_SelectionChanged( object sender , SelectionChangedEventArgs e )
        {
            try
            {
                if ( e.AddedItems.Count > 0 )
                {
                    Autoteile selected = ( ( sender as ListView ).SelectedItem as Autoteile );
                    ( this.root.DataContext as MainWindowViewModel ).SetTeilInTxt(selected);

                }
                else
                {
                    throw ( new Exception("nothing selected") );
                }
            }
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);

            }
        }

        private void OnWindowClosing( object sender , CancelEventArgs e )
        {
            ( this.root.DataContext as MainWindowViewModel ).ShutThread();
        }

        private void Window_Initialized( object sender , EventArgs e )
        {
            ( this.root.DataContext as MainWindowViewModel ).MainWindow = this;
        }

        private void Window_Loaded( object sender , RoutedEventArgs e )
        {
            string s = string.Empty;
            int min = default(int);
            if ( Environment.GetCommandLineArgs()?.Any(item =>
              item != null && ( bool ) item?.Equals("--configure")) == true )
            {
                SettingsManager.Instance.ShowEditor();
                CongifManager.UpdateSettings(SettingsManager.Instance.GetSettings());
            }
            else if ( Environment.GetCommandLineArgs()?.Any(item =>
                item != null && ( bool ) item?.Equals("--basic")) == true )
            {
                BasicSettings bs = new BasicSettings();
                bs.ShowDialog();
                s = bs.GetName();
                min = bs.GetMin();
            }

            ( this.root.DataContext as MainWindowViewModel ).Init(s , min);
            ( this.root.DataContext as MainWindowViewModel ).TeilChanged = teilChanged;
            ( this.root.DataContext as MainWindowViewModel ).TeilNotOk = teilNotOk;
            ( this.root.DataContext as MainWindowViewModel ).TeilOk = teilOK;
        }

        private List<int> kritischTeileIdx = new List<int>();

        private void teilNotOk( Autoteile teil )
        {
            Autoteile a = this.lvTeile.Items.Cast<Autoteile>().First(item => item.Bezeichnung.Equals(teil.Bezeichnung));
            int idx = this.lvTeile.Items.IndexOf(a);
            kritischTeileIdx.Add(idx);
        }
        private void teilChanged( bool toggle )
        {
            ResourceDictionary res = ( ResourceDictionary ) Application.LoadComponent(new Uri("Resources/ResourceDictionary.xaml" , UriKind.Relative));
            string r = string.Empty;
            if ( toggle )
                r = "noBorder";
            else
                r = "borderRed";
            foreach ( var item in this.kritischTeileIdx )
            {
                ( this.lvTeile.ItemContainerGenerator.ContainerFromIndex(item) as ListViewItem ).Style = ( Style ) res[r];
            }
        }

        private void teilOK( string bezeichnung )
        {
            Autoteile a = this.lvTeile.Items.Cast<Autoteile>().First(item => item.Bezeichnung.Equals(bezeichnung));
            int idx = this.lvTeile.Items.IndexOf(a);
            kritischTeileIdx.Remove(idx);
        }
    }
}
