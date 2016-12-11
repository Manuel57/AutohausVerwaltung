// <copyright file="Benutzerverwaltung.MainWindow.xaml.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-07</date>
// </copyright>

using Benutzerverwaltung.Controls;
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
using Verwaltung.Settings;

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

            this.lvCustomer.ItemsSource = cs.Where(item => 
            item.Adress.Contains(this.tbSearch.Text) ||
                item.BirthDate.ToShortDateString().Contains(this.tbSearch.Text) ||
                item.CustomerId.ToString().Contains(this.tbSearch.Text) ||
                item.FirstName.Contains(this.tbSearch.Text) ||
                item.LastName.Contains(this.tbSearch.Text) ||
                item.Username.Contains(this.tbSearch.Text));
        }

        private void Window_Loaded( object sender , RoutedEventArgs e )
        {

            if ( Environment.GetCommandLineArgs()?.Any(item =>
              item != null && ( bool ) item?.Equals("--configure")) == true )
            {
                SettingsManager.Instance.ShowEditor();
            }
            ( this.root.DataContext as MainWindowViewModel ).Init();

        }

        private void Button_Click( object sender , RoutedEventArgs e )
        {
            MessageBox.Show(( (sender as Button ).Parent as StackPanel).Parent.ToString());
        }

        private void CustomerButton_Click( object sender , RoutedEventArgs e )
        {
            ( this.root.DataContext as MainWindowViewModel ).ShowCustomerDetails(( int ) sender);
        }

        private void CustomerButtonDel_Click( object sender , RoutedEventArgs e )
        {

            ( this.root.DataContext as MainWindowViewModel ).DeleteCustomer(( int ) sender);
        }
    }
}
