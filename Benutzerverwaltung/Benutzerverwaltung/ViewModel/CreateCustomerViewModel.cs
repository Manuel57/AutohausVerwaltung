// <copyright file="Benutzerverwaltung.ViewModel.CreateCustomerViewModel.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-12</date>
// </copyright>

using Benutzerverwaltung.Helpers;
using Benutzerverwaltung.Model;
using Benutzerverwaltung.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benutzerverwaltung.ViewModel
{
    public class CreateCustomerViewModel : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public string Address { get; set; }
        public RelayCommand CreateCommand { get; set; }
        public RelayCommand BackCommand { get; set; }

        public CreateCustomerViewModel( )
        {
            this.BackCommand = new RelayCommand(( ) => { });
            this.CreateCommand = new RelayCommand(( ) => { new UserInfoView().ShowDialog(); });
        }
    }
}
