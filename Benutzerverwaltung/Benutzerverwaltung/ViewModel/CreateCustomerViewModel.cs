// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-12</date>

using Benutzerverwaltung.Helpers;
using Benutzerverwaltung.Model;
using Benutzerverwaltung.View;
using BenutzerverwaltungBL.Controller;
using BenutzerverwaltungBL.Model.DataObjects;
using System;
using Verwaltung.Exception;

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
                ExceptionHelper.Handle(ex);
            }

        }

        /// <summary>
        /// Creates a new Customer
        /// </summary>
        private void create( )
        {
            try
            {
                if ( string.IsNullOrEmpty(this.Address) ||
                    string.IsNullOrEmpty(this.FirstName) ||
                    string.IsNullOrEmpty(this.LastName) )
                    throw new Exception("Nicht alle Werte eingegeben");
                DateTime bd;
                try
                {
                    bd = DateTime.Parse(BirthDate);

                }
                catch ( Exception e )
                {
                    throw new Exception("Datumsformat nicht korrekt!");
                }
                Customer c = CustomerManager.CreateCustomer("" , this.FirstName , this.LastName , bd , this.Address);
                UserInfoView uiv = new UserInfoView(c);
                uiv.ShowDialog();

            }
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }

        }
    }
}
