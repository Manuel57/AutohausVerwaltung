// <copyright file="Benutzerverwaltung.View.ReparaturenView.xaml.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-12-6</date>
// </copyright>

using BenutzerverwaltungBL.Controller;
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
    /// Interaction logic for ReparaturenView.xaml
    /// </summary>
    public partial class ReparaturenView : Window
    {
        public ReparaturenView( )
        {
            InitializeComponent();
        }
        public ReparaturenView(string customerId ):this()
        {
            this.listView.ItemsSource = CustomerManager.GetSingleCustomerById(int.Parse(customerId)).Rechnungen;
        }
    }
}
