﻿// <copyright file="Benutzerverwaltung.ViewModel.MainWindowViewModel.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-12</date>
// </copyright>

using Benutzerverwaltung.Helpers;
using Benutzerverwaltung.Model;
using Benutzerverwaltung.View;
using BenutzerverwaltungBL.Configuration;
using BenutzerverwaltungBL.Controller;
using BenutzerverwaltungBL.Model.DataObjects;
using Remotion.Linq.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Benutzerverwaltung.ViewModel
{
    public class MainWindowViewModel : ModelBase
    {
        public string TextSearchField { get; set; }
        public ObservableCollection<Customer> Emp { get; set; }
        public RelayCommand DetailsCustomerCommand { get; set; }
        public RelayCommand DeleteCustomerCommand { get; set; }
        public RelayCommand CreateCustomerCommand { get; set; }
        public RelayCommand SearchFieldChanged { get; set; }

        public MainWindowViewModel( )
        {
            ConfigureBl.Initialize();
            this.Emp = new ObservableCollection<Customer>();
            this.DeleteCustomerCommand = new RelayCommand(this.deleteCustomer);
            this.CreateCustomerCommand = new RelayCommand(this.createCustomer);
            this.DetailsCustomerCommand = new RelayCommand(this.showCustomerDetails);
            this.SearchFieldChanged = new RelayCommand(( ) => { MessageBox.Show("Changed"); });
           
            updateView();
        }

        private void deleteCustomer( ) {
            CustomerDetailsView cdv = new CustomerDetailsView(CustomerDetailsMode.Delete);
            cdv.Show();
        }
        private void showCustomerDetails( )
        {
            MessageBox.Show("TEST");
            CustomerDetailsView cdv = new CustomerDetailsView(CustomerDetailsMode.Details);
            cdv.Show();
        }
        private void createCustomer( )
        {
            CreateCustomerView ccv = new CreateCustomerView();
            ccv.ShowDialog();
            updateView();
        }

        private void updateView()
        {
            this.Emp = new ObservableCollection<Customer>();
            foreach ( Customer c in CustomerManager.GetAllCustomers() )
            {
                this.Emp.Add(c);
            }
            this.OnPropertyChanged("Emp");
            this.OnPropertyChanged();
        }
    }
}
