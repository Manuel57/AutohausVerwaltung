// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-12</date>

using Benutzerverwaltung.Helpers;
using Benutzerverwaltung.Model;
using Benutzerverwaltung.View;
using BenutzerverwaltungBL.Configuration;
using BenutzerverwaltungBL.Controller;
using BenutzerverwaltungBL.Model.DataObjects;
using Remotion.Linq.Collections;
using System;
using System.Linq;
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
            this.CreateCustomerCommand = new RelayCommand(this.createCustomer);
            this.SearchFieldChanged = new RelayCommand(( ) => { MessageBox.Show("Changed"); });
        }

        /// <summary>
        /// Initializes the business logic and updates the view
        /// </summary>
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
        /// <summary>
        /// Shows the Detals window in Delete mode
        /// </summary>
        /// <param name="CustId">The customer id</param>
        public void DeleteCustomer( int CustId )
        {
            try
            {
                CustomerDetailsView cdv = new CustomerDetailsView(
                    CustomerDetailsMode.Delete , this.Emp.DefaultIfEmpty(null).
                    FirstOrDefault(item => item.CustomerId.Equals(CustId)));
                cdv.ShowDialog();
                updateView();
            }
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }
        }
        /// <summary>
        /// Shows the details view in edit mode
        /// </summary>
        /// <param name="CustId">The customer id</param>
        public void ShowCustomerDetails( int CustId )
        {
            try
            {
                CustomerDetailsView cdv = new CustomerDetailsView(
                    CustomerDetailsMode.Details , this.Emp.DefaultIfEmpty(null)
                    .FirstOrDefault(item => item.CustomerId.Equals(CustId)));
                cdv.Show();
            }
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }
        }
        /// <summary>
        /// Shows the create customer view and updates the view
        /// </summary>
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

        /// <summary>
        /// Updates the view
        /// </summary>
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
