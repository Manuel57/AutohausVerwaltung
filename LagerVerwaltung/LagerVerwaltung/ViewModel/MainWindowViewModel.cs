using LagerVerwaltung.Helpers;
using LagerVerwaltung.Model;
using Remotion.Linq.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerVerwaltung.ViewModel
{
    public class MainWindowViewModel:ModelBase
    {
        private List<Autoteile> stattController = null;
        public ObservableCollection<Autoteile> allTeile { get; set; }
        //sollt iwi mit an Thread upgedated werden
        public ObservableCollection<Message> importantMessages { get; set; }
       // public RelayCommand ListViewSeletionChanged { get; set; }
        public RelayCommand ChangeTeilCommand { get; set; }
        public RelayCommand CreateTeilCommand { get; set; }
        public RelayCommand OrderTeilCommand { get; set; }
        public RelayCommand DetailMessageCommand { get; set; }

        public MainWindowViewModel()
        {
            this.allTeile = new ObservableCollection<Autoteile>();
            this.importantMessages = new ObservableCollection<Message>();
            this.ChangeTeilCommand = new RelayCommand(this.ChangeTeil);
            this.CreateTeilCommand = new RelayCommand(this.CreateTeil);
            this.OrderTeilCommand = new RelayCommand(this.OrderTeil);
            DetailMessageCommand = new RelayCommand(ShowDetailsOfMessage);
            FillView();
        }

        private void ShowDetailsOfMessage()
        {
            //brauch ma vlt gar nit besser wär de ganze messge glei anzeigen wsl
        }
        private void CreateTeil ()
        {

        }

        private void OrderTeil()
        {

        }

        private void ChangeTeil()
        {

        }
        public int ListViewTeileChanged(Autoteile selected)
        {
            //den lagerbestand abrufen aus der db
            return 200;
        }

        private void FillView()
        {
            createTeile();
            foreach(Autoteile a in stattController)
            {
                this.allTeile.Add(a);
            }
            this.OnPropertyChanged("allTeile");
            this.OnPropertyChanged();
        }

        private void createTeile()
        {
            this.stattController = new List<Autoteile>();
            Autoteile reifen = new Autoteile() { Bezeichnung = "Reifen", Preis = 400 };
            Autoteile vergaser = new Autoteile() { Bezeichnung = "Vergaser", Preis = 310 };
            Autoteile schraube = new Autoteile() { Bezeichnung = "Schraube12x10", Preis = 1 };
            stattController.Add(reifen);
            stattController.Add(vergaser);
            stattController.Add(schraube);
        }
    }
}
