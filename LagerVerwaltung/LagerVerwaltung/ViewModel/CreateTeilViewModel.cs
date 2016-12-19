using LagerVerwaltung.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verwaltung.Exception;

namespace LagerVerwaltung.ViewModel
{
   public  class CreateTeilViewModel
    {
        public string Bezeichnung { get; set; }
        public string Preis { get; set; }
        public RelayCommand CreateTeilCommand { get; set; }

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
                //manager.create
                if(!string.IsNullOrEmpty(this.Preis) && !string.IsNullOrEmpty(this.Bezeichnung))
                throw (new Exception("Teil " + this.Bezeichnung + " mit Preis: " + this.Preis + " erstellt"));
            }
            catch(Exception e)
            {
                ExceptionHelper.Handle(e);
            }
        }
    }
}
