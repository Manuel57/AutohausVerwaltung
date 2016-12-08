// <copyright file="Benutzerverwaltung.ViewModel.CreateCustomerViewModel.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-12</date>
// </copyright>

using Benutzerverwaltung.Helpers;
using Benutzerverwaltung.Model;
using Benutzerverwaltung.View;
using BenutzerverwaltungBL.Controller;
using BenutzerverwaltungBL.Model.DataObjects;
using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Benutzerverwaltung.ViewModel
{
    public class CreateCustomerViewModel : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Address { get; set; }
        public RelayCommand CreateCommand { get; set; }
        public RelayCommand BackCommand { get; set; }

        public CreateCustomerViewModel( )
        {

            try
            {
                this.BackCommand = new RelayCommand(( ) => { });
                this.CreateCommand = new RelayCommand(create);
            }
            catch ( Exception ex )
            {
                ExceptionManager.Instance.Handle(ex);
            }

        }

        private void create( )
        {
            try
            {
                Customer c = CustomerManager.CreateCustomer("" , this.FirstName , this.LastName , DateTime.Parse(BirthDate) , this.Address);
                UserInfoView uiv = new UserInfoView(c);
                uiv.ShowDialog();
            }
            catch ( Exception ex )
            {
                ExceptionManager.Instance.Handle(ex);
            }

        }
    }
}
