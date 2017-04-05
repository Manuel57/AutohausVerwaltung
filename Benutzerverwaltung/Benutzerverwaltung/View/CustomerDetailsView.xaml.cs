// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-29</date>

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
using Verwaltung;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Verwaltung.Exception;

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
        /// <summary>
        /// Creates the View in the given mode
        /// </summary>
        /// <param name="mode">The mode</param>
        /// <param name="c">The customer</param>
        public CustomerDetailsView( CustomerDetailsMode mode , Customer c )
        {
            InitializeComponent();

            try
            {
                ResourceDictionary res = ( ResourceDictionary ) Application.LoadComponent(new Uri("Resources/ResourceDictionary.xaml" , UriKind.Relative));
                initVm(c);
                Button btn = new Button();
                btn.Style = ( Style ) res["textBoxStyle"];
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
                btnReparatur.Style = ( Style ) res["textBoxStyle"];
                btnReparatur.Content = Properties.Resources.Reparaturen;
                btnReparatur.Command = ( this.root.DataContext as DetailsViewModel ).ReparaturenCommand;
                Grid.SetRow(btnReparatur , 6);
                Grid.SetColumn(btnReparatur , 0);
                this.root.Children.Add(btnReparatur);
                this.root.Children.Add(btn);
            }
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }

        }
        /// <summary>
        /// Initializes the Viewmodel
        /// </summary>
        /// <param name="c">The current customer</param>
        private void initVm( Customer c )
        {
            try
            {
                ( this.root.DataContext as DetailsViewModel ).Kunde = c;
                ( this.root.DataContext as DetailsViewModel ).ChangedAll();
                ( this.root.DataContext as DetailsViewModel ).Close = ( ) => { this.Close(); };
            }
            catch ( Exception )
            {
                throw;
            }


        }
        /// <summary>
        /// Sets all textboxes readonly
        /// </summary>
        private void readonlyTextBoxes( )
        {

            try
            {
                this.txtAddress.ReadOnly();
                this.txtBirthDate.ReadOnly();
                this.txtCustomerId.ReadOnly();
                this.txtFirstName.ReadOnly();
                this.txtLastName.ReadOnly();
                this.txtUsername.ReadOnly();
            }
            catch ( Exception )
            {
                throw;
            }

        }
    }
}
