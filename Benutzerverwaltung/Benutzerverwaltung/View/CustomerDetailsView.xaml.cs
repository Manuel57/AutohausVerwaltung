// <copyright file="Benutzerverwaltung.View.CustomerDetailsView.xaml.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-29</date>
// </copyright>

using Benutzerverwaltung.Helpers;
using Benutzerverwaltung.ViewModel;
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
        public CustomerDetailsView( CustomerDetailsMode mode, Customer c )
        {
            InitializeComponent();
            initVm(c);
            Button btn = new Button();
            switch ( mode )
            {
                case CustomerDetailsMode.Delete:
                    btn.Content = "Delete";
                    btn.Command = ( this.root.DataContext as DetailsViewModel ).DeleteCommand;
                    this.readonlyTextBoxes();
                    break;
                case CustomerDetailsMode.Details:
                    btn.Content = "Save changes";
                    btn.Command = ( this.root.DataContext as DetailsViewModel ).ChangeCommand;
                    
                    break;
                default:
                    break;
            }
            Grid.SetRow(btn , 6);
            Grid.SetColumn(btn , 1);

            Button btnReparatur = new Button();
            btnReparatur.Content = Properties.Resources.Reparaturen;
            btnReparatur.Command = ( this.root.DataContext as DetailsViewModel ).ReparaturenCommand;
            Grid.SetRow(btnReparatur , 6);
            Grid.SetColumn(btnReparatur , 0);
            this.root.Children.Add(btnReparatur);
            this.root.Children.Add(btn);
        }
        private void initVm(Customer c)
        {
            ( this.root.DataContext as DetailsViewModel ).Address = c.Adress;
            ( this.root.DataContext as DetailsViewModel ).BirthDate = c.BirthDate.ToShortDateString();
            ( this.root.DataContext as DetailsViewModel ).CustomerId = c.CustomerId.ToString();
            ( this.root.DataContext as DetailsViewModel ).FirstName = c.FirstName;
            ( this.root.DataContext as DetailsViewModel ).LastName = c.LastName;
            ( this.root.DataContext as DetailsViewModel ).Username = c.Username;
            ( this.root.DataContext as DetailsViewModel ).ChangedAll();

        }

        private void readonlyTextBoxes( )
        {
            this.txtAddress.ReadOnly();
            this.txtBirthDate.ReadOnly();
            this.txtCustomerId.ReadOnly();
            this.txtFirstName.ReadOnly();
            this.txtLastName.ReadOnly();
            this.txtUsername.ReadOnly();
        }
    }
}
