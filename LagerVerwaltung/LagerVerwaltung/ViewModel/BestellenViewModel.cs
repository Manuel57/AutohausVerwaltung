using LagerVerwaltung.Helpers;
using LagerVerwaltung.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verwaltung.Exception;

namespace LagerVerwaltung.ViewModel
{
    public class BestellenViewModel: ModelBase
    {
        public string PartToOrder { get; set; }
        public string Preis { get; set; }
        public string Menge { get; set; } = "0";
        public RelayCommand OrderTeilCommand { get; set; }
        public Autoteile selected { get; set; }

        public BestellenViewModel()
        {
            OrderTeilCommand = new RelayCommand(order);
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
              
                throw (new Exception("ordered " + this.PartToOrder + " " + this.Menge));
            }
           
            catch (Exception ex)
            {
                ExceptionHelper.Handle(ex);
            }

        }
    }
}
