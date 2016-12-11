// <copyright file="Benutzerverwaltung.ViewModel.MainWindowViewModel.cs">
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
using Verwaltung.Exception;

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


            this.Emp = new ObservableCollection<Customer>();
            //this.DeleteCustomerCommand = new RelayCommand(this.deleteCustomer);
            this.CreateCustomerCommand = new RelayCommand(this.createCustomer);
            //this.DetailsCustomerCommand = new RelayCommand(this.showCustomerDetails);
            this.SearchFieldChanged = new RelayCommand(( ) => { MessageBox.Show("Changed"); });

        }
        public void Init( )
        {
            try
            {
                ConfigureBl.Initialize();

                updateView();
            }
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }
        }

        public void DeleteCustomer(int CustId )
        {
            try
            {
                CustomerDetailsView cdv = new CustomerDetailsView(CustomerDetailsMode.Delete , this.Emp.DefaultIfEmpty(null).FirstOrDefault(item => item.CustomerId.Equals(CustId)));
                cdv.Show();
            }
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }
        }
        public void ShowCustomerDetails(int CustId)
        {
            try
            {
                CustomerDetailsView cdv = new CustomerDetailsView(CustomerDetailsMode.Details , this.Emp.DefaultIfEmpty(null).FirstOrDefault(item=>item.CustomerId.Equals(CustId)));
                cdv.Show();
            }
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }
        }
        private void createCustomer( )
        {
            try
            {
                CreateCustomerView ccv = new CreateCustomerView();
                ccv.ShowDialog();
                updateView();
            }
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }
        }

        private void updateView( )
        {
            try
            {
                this.Emp = new ObservableCollection<Customer>();
                foreach ( Customer c in CustomerManager.GetAllCustomers() )
                {
                    this.Emp.Add(c);
                }
                this.OnPropertyChanged("Emp");
                this.OnPropertyChanged();
            }
            catch ( Exception )
            {
                throw;
            }
        }
    }
}
