// <copyright file="Benutzerverwaltung.MainWindow.xaml.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-07</date>
// </copyright>

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
        public MainWindow()
        {
            InitializeComponent();
            //This is my first comment
            
        }

       

        private void listboxFolder1_SelectionChanged( object sender , SelectionChangedEventArgs e )
        {
        }

        /// <summary>
        /// test values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            ConfigureBl.Initialize();
            CustomerManager.CreateCustomer("as", "Lackenbucher Manuel", DateTime.Today, "9500 Villach");
            CustomerManager.CreateCustomer("as", "Huber Thomas", DateTime.Today, "9560 Feldkirchen");
            IEnumerable<Customer> customers = CustomerManager.GetAllCustomers();
            MessageBox.Show(customers.Count().ToString());

            Customer c = CustomerManager.GetSingleCustomerById(1);
            MessageBox.Show(c.FullName + " " + c.Adress);
        }
    }
}
