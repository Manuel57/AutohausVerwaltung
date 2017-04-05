using LagerVerwaltung.Helpers;
using LagerVerwaltung.Model;
using LagerverwaltungBL.Controller;
using LagerverwaltungBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Verwaltung.Exception;

namespace LagerVerwaltung.ViewModel
{
    public class BestellenViewModel: ModelBase
    {
        private string Werkstatt { get; set; }
        public string PartToOrder { get; set; }
        public string Preis { get; set; }
        public string Menge { get; set; } = "0";
        public RelayCommand OrderTeilCommand { get; set; }
        private Autoteile selected { get; set; }

        public BestellenViewModel()
        {
            OrderTeilCommand = new RelayCommand(order);
           
        }
       
        public void init(string Werkstatt,Autoteile selected)
        {
            this.Werkstatt = Werkstatt;
            this.selected = selected;
        }
        public void TeilChanged()
        {
            this.PartToOrder = selected.Bezeichnung;
            this.Preis = selected.Preis.ToString()+"€";
            this.propertyChanged("PartToOrder","Preis");
        }

        private void propertyChanged(params string[] properties)
        {
            foreach (var prop in properties)
            {
                this.OnPropertyChanged(prop);
            }
            this.OnPropertyChanged();
        }

        private void order()
        {
            //menge und bezeichnung abfragen und mit controller orderen
            try
            {
                if (string.IsNullOrEmpty(this.Menge))
                {
                    throw (new Exception("no amount to order!\n " + this.PartToOrder + " " + this.Menge));
                }

                TeileManager.Order(this.PartToOrder, this.Werkstatt, null, int.Parse(this.Menge));
                MessageBox.Show("Successfully orderd " + this.Menge + " of " + this.PartToOrder+"!");
            }
           
            catch (Exception ex)
            {
                ExceptionHelper.Handle(ex);
            }

        }
    }
}
