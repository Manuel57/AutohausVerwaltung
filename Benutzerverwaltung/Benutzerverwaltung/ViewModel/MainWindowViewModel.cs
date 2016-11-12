// <copyright file="Benutzerverwaltung.ViewModel.MainWindowViewModel.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-12</date>
// </copyright>

using Benutzerverwaltung.Helpers;
using Benutzerverwaltung.Model;
using Benutzerverwaltung.View;
using Remotion.Linq.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benutzerverwaltung.ViewModel
{
    public class Emps
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
    public class MainWindowViewModel : ModelBase
    {
        public string TextSearchField { get; set; }
        public ObservableCollection<Emps> Emp { get; set; }
        public RelayCommand DetailsCustomerCommand { get; set; }
        public RelayCommand DeleteCustomerCommand { get; set; }
        public RelayCommand CreateCustomerCommand { get; set; }

        public MainWindowViewModel( )
        {
            this.Emp = new ObservableCollection<Emps>();
            this.DeleteCustomerCommand = new RelayCommand(this.deleteCustomer);
            this.CreateCustomerCommand = new RelayCommand(this.createCustomer);
            this.DetailsCustomerCommand = new RelayCommand(this.showCustomerDetails);


            Emp.Add(new Emps() { Name = "A" , Id = 1 });
            Emp.Add(new Emps() { Name = "B" , Id = 2 });
            Emp.Add(new Emps() { Name = "C" , Id = 3 });
            Emp.Add(new Emps() { Name = "D" , Id = 4 });
            Emp.Add(new Emps() { Name = "E" , Id = 5 });
            Emp.Add(new Emps() { Name = "F" , Id = 6 });
            Emp.Add(new Emps() { Name = "G" , Id = 7 });
            Emp.Add(new Emps() { Name = "H" , Id = 8 });
        }

        private void deleteCustomer( ) { }
        private void showCustomerDetails( ) { }
        private void createCustomer( )
        {
            System.Windows.MessageBox.Show("");
            CreateCustomerView ccv = new CreateCustomerView();
            ccv.ShowDialog();
        }
    }
}
