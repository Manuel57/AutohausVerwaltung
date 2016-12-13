using LagerVerwaltung.Helpers;
using LagerVerwaltung.Model;
using LagerVerwaltung.View;
using Remotion.Linq.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Verwaltung.Exception;

namespace LagerVerwaltung.ViewModel
{
    public class MainWindowViewModel : ModelBase
    {
        private List<Autoteile> stattController = null;
        private Thread messageThread = null;
        private bool shutDownThread = false;
        private static double threadSlepp = 1;
        public ObservableCollection<Autoteile> allTeile { get; set; }
        //sollt iwi mit an Thread upgedated werden
        public ObservableCollection<Message> importantMessages { get; set; }
        // public RelayCommand ListViewSeletionChanged { get; set; }
        public RelayCommand ChangeTeilCommand { get; set; }
        public RelayCommand CreateTeilCommand { get; set; }
        public RelayCommand OrderTeilCommand { get; set; }
        public Window Mw;
        public event EventHandler<EventArgs> Kritisch;


        public MainWindowViewModel( )
        {
            this.allTeile = new ObservableCollection<Autoteile>();
            this.importantMessages = new ObservableCollection<Message>();
            this.ChangeTeilCommand = new RelayCommand(this.ChangeTeil);
            this.CreateTeilCommand = new RelayCommand(this.CreateTeil);
            this.OrderTeilCommand = new RelayCommand(this.OrderTeil);
            this.messageThread = new Thread(GetMessages);
            this.Kritisch += test;
            messageThread.Start();
            FillView();
        }



        private void CreateTeil( )
        {
            try
            {
                CreateTeilView cv = new CreateTeilView();
                cv.ShowDialog();
                FillView();
            }
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }
        }

        private void OrderTeil( )
        {
            try
            {
                string selected = null;
                foreach ( Window w in Application.Current.Windows )
                {
                    if ( w.GetType() == typeof(MainWindow) )
                    {
                        selected = ( w as MainWindow ).txtSelected.Text;
                    }
                }
                if ( string.IsNullOrEmpty(selected) )
                {
                    throw new Exception("Nothing selected to order!");
                }

                BestellenView bv = new BestellenView(allTeile.ToList().Find(item => item.Bezeichnung == selected));
                bv.ShowDialog();
            }
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }
        }

        //i glab des brauch ma gar nit oda?
        private void ChangeTeil( )
        {
            try
            {
                throw new NotImplementedException();
            }
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }
        }
        private void GetMessages( )
        {
            try
            {
                while ( !shutDownThread )
                {
                    this.importantMessages.Clear();
                    //get alle teile wo der lager bestand kritisch is vom controller
                    for ( int i = 0; i < 2; i++ )
                    {

                        //this.importantMessages.Add(new Message() { Short = "Lagerbestand von Reifen  kritisch!\nBestand: " + DateTime.Now.Minute + 100 });
                    }
                    if (this.Mw!=null)
                    {
                        this.Mw.Dispatcher.Invoke(( ) =>
                        {
                            MessageBox.Show("IN invoke");
                            ObservableCollection<Message> m = new ObservableCollection<Message>();
                            m.Add(new Message() { Short = "TEEEEST MEEE" + DateTime.Now.Second });
                            this.importantMessages = m;
                        });
                        Kritisch(this , null);
                    }
                    //geht nicht
                    //if ( Mw != null )
                    //{
                    //    this.Mw.Dispatcher.Invoke(( ) =>
                    //    {
                    //        this.OnPropertyChanged("importantMessages");
                    //        this.OnPropertyChanged();
                    //    });
                    //    MessageBox.Show("IN");
                    //}
                    Thread.Sleep(TimeSpan.FromMinutes(threadSlepp));
                }
            }

            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }
        }
        private void test( object s , EventArgs e )
        {
            this.OnPropertyChanged("importantMessages");
            this.OnPropertyChanged();
            MessageBox.Show(this.importantMessages.Count.ToString());
        }
        internal string GetBestandForTeil( Autoteile selected )
        {
            //controller. getbestand(selected) ....
            return 2000.ToString();
        }

        private void FillView( )
        {
            createTeile();
            foreach ( Autoteile a in stattController )
            {
                this.allTeile.Add(a);
            }
            this.OnPropertyChanged("allTeile");
            this.OnPropertyChanged();
        }

        private void createTeile( )
        {
            this.stattController = new List<Autoteile>();
            Autoteile reifen = new Autoteile() { Bezeichnung = "Reifen" , Preis = 400 };
            Autoteile vergaser = new Autoteile() { Bezeichnung = "Vergaser" , Preis = 310 };
            Autoteile schraube = new Autoteile() { Bezeichnung = "Schraube12x10" , Preis = 1 };
            stattController.Add(reifen);
            stattController.Add(vergaser);
            stattController.Add(schraube);
        }

        public void shutThread( )
        {
            try
            {
                if ( this.messageThread.IsAlive )
                {
                    this.shutDownThread = true;
                    // this.messageThread.Abort();
                }
            }
            catch ( Exception e )
            {
                ExceptionHelper.Handle(e);
            }
        }
    }
}
