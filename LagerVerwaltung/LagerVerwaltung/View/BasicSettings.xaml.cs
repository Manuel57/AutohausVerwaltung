using LagerverwaltungBL.Configuration;
using LagerverwaltungBL.Controller;
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
using Verwaltung.Exception;

namespace LagerVerwaltung.View
{
    /// <summary>
    /// Interaktionslogik für BasicSettings.xaml
    /// </summary>
    public partial class BasicSettings : Window
    {
        private string selected = "";
        private int min = default(int);
        public BasicSettings()
        {
            InitializeComponent();
            CongifManager.Initialize();
            FillComboBox();
        }

        /// <summary>
        /// fills the combobox
        /// </summary>
        private void FillComboBox()
        {

            try
            {
                this.cmbWerkstatt.ItemsSource = TeileManager.AllWerkstattNames();
                cmbWerkstatt.SelectedItem = 1;
            }
           
            catch (Exception ex)
            {
                ExceptionHelper.Handle(ex);
            }

        }

        private void cmbWerkstatt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                this.selected = (sender as ComboBox).SelectedItem.ToString();
            }
           
            catch (Exception ex)
            {
                ExceptionHelper.Handle(ex);
            }

        }

        public string GetName()
        {
            return this.selected;
        }
        public int GetMin()
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(this.txtMin.Text))
                {
                    this.min = int.Parse(this.txtMin.Text);
                }
            }
           
            catch (Exception )
            {
                
            }
            return this.min;
        }
    }
}
