// <copyright file="Benutzerverwaltung.View.CustomerDetailsView.xaml.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-29</date>
// </copyright>

using Benutzerverwaltung.Helpers;
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
using System.Windows.Shapes;

namespace Benutzerverwaltung.View
{
    /// <summary>
    /// Interaction logic for CustomerDetailsView.xaml
    /// </summary>
    public partial class CustomerDetailsView : Window
    {
        public CustomerDetailsView( )
        {
            InitializeComponent();
        }
        public CustomerDetailsView( CustomerDetailsMode mode )
        {
            InitializeComponent();
            Button btn = new Button();
            switch ( mode )
            {
                case CustomerDetailsMode.Delete:
                    btn.Content = "Delete";
                    btn.Click += ( object sender , RoutedEventArgs args ) => { MessageBox.Show("DELETE"); };
                    break;
                case CustomerDetailsMode.Details:
                    btn.Content = "Save changes";
                    btn.Click += ( object sender , RoutedEventArgs args ) => { MessageBox.Show("SAVE"); };

                    break;
                default:
                    break;
            }
            this.mainGrid.Children.Add(btn);
        }
    }
}
