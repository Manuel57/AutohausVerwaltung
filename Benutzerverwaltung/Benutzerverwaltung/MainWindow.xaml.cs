// <copyright file="Benutzerverwaltung.MainWindow.xaml.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-07</date>
// </copyright>

using Benutzerverwaltung.Helpers;
using Benutzerverwaltung.View;
using Benutzerverwaltung.ViewModel;
using BenutzerverwaltungBL.Configuration;
using BenutzerverwaltungBL.Controller;
using BenutzerverwaltungBL.Model.DataObjects;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Benutzerverwaltung
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow( )
        {
            InitializeComponent();


        }



        private void listboxFolder1_SelectionChanged( object sender , SelectionChangedEventArgs e )
        {
        }

        private void lvCustomer_SelectionChanged( object sender , SelectionChangedEventArgs e )
        {
            MessageBox.Show("");
        }

        private void tbSearch_TextChanged( object sender , TextChangedEventArgs e )
        {
            IEnumerable<Customer> cs = this.lvCustomer.ItemsSource as IEnumerable<Customer>;
            this.lvCustomer.ItemsSource = null;
            this.lvCustomer.ItemsSource = cs.Take(1);
        }

        private void Window_Loaded( object sender , RoutedEventArgs e )
        {

            if ( Environment.GetCommandLineArgs()?.Any(item =>
              item != null && ( bool ) item?.Equals("--configure")) == true )
            {
                SettingsManager.ShowDialog();
            }
            ( this.root.DataContext as MainWindowViewModel ).Init();
        }
    }
}
