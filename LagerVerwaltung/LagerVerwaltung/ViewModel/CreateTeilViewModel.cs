using LagerVerwaltung.Helpers;
using LagerverwaltungBL.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Verwaltung.Exception;

namespace LagerVerwaltung.ViewModel
{
   public  class CreateTeilViewModel
    {
        public string Bezeichnung { get; set; }
        public string Preis { get; set; }
        public RelayCommand CreateTeilCommand { get; set; }
        public Action close { get; set; }
        public CreateTeilViewModel()
        {

            try
            {
                this.CreateTeilCommand = new RelayCommand(create);
            }
            
            catch (Exception e)
            {
                ExceptionHelper.Handle(e);
            }

        }

        private void create()
        {
            try
            {                
                if(!string.IsNullOrEmpty(this.Preis) && !string.IsNullOrEmpty(this.Bezeichnung))
                {
                    double preis = double.Parse(this.Preis);
                    MessageBox.Show("successfully created "+preis);
                    TeileManager.CreateAutoteil(this.Bezeichnung, preis);
                   
                    this.close();
                }

                
            }
            catch(Exception e)
            {
                ExceptionHelper.Handle(e);
            }
        }
    }
}
