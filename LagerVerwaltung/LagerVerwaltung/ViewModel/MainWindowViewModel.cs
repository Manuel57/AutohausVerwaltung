﻿using LagerVerwaltung.Helpers;
using LagerVerwaltung.Model;
using LagerVerwaltung.View;
using LagerverwaltungBL.Configuration;
using LagerverwaltungBL.Controller;
using LagerverwaltungBL.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        #region private fields
        private static double threadSlepp = 0.5;
       // private List<Autoteile> stattController = null;
        private Thread messageThread = null;
        private bool shutDownThread = false;
        private static string WERKSTATT = "Villach";
        private static int MINBESTAND = 100;
        private bool toggle = false;
        private DispatcherTimer timer;
        #endregion

        #region public fields
        public Action<bool> TeilChanged { get; set; }
        public ObservableCollection<Autoteile> allTeile { get; set; }
        public ObservableCollection<Message> importantMessages { get; set; }
        public RelayCommand CreateTeilCommand { get; set; }
        public RelayCommand OrderTeilCommand { get; set; }
        public Window MainWindow { set; private get; }
        public string PartToOrder { get; set; }
        public string Preis { get; set; }
        public string Bestand { get; set; }
        public Action<Autoteile> TeilNotOk { get; internal set; }
        public Action<string> TeilOk { get; internal set; }
       
        #endregion
        public MainWindowViewModel()
        {
            this.allTeile = new ObservableCollection<Autoteile>();
            this.importantMessages = new ObservableCollection<Message>();
            this.importantMessages.CollectionChanged += messagesChanged;
            this.CreateTeilCommand = new RelayCommand(this.createTeil);
            this.OrderTeilCommand = new RelayCommand(this.orderTeil);
            this.messageThread = new Thread(getMessages);
           
            this.timer = new DispatcherTimer();
            this.timer.Tick += timerTick;
            this.timer.Interval = TimeSpan.FromMilliseconds(500);
         
        }
      
        private void timerTick( object sender , EventArgs e )
        {
            this.toggle = !toggle;
            this.TeilChanged(toggle);
        }
        #region public methods
        public void Init(string name, int min)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(name))
                {
                    WERKSTATT = name;
                }
                if(min != default(int))
                {
                    MINBESTAND = min;
                }

                if(string.IsNullOrEmpty(name))
                {
                    CongifManager.Initialize();
                }
               
                fillView();
                messageThread.Start();

            }
            catch (Exception ex)
            {
                ExceptionHelper.Handle(ex);
            }
        }

        public void SetTeilInTxt(Autoteile a)
        {

            try
            {
                this.PartToOrder = a.Bezeichnung;
                this.Preis = a.Preis.ToString();
                this.Bestand = getBestandForTeil(a);
                this.propertyChanged("Preis", "PartToOrder", "Bestand");
            }
           
            catch (Exception )
            {
                throw;
            }

        }

        public void ShutThread()
        {
            try
            {
                if (this.messageThread.IsAlive)
                {
                    this.shutDownThread = true;
                    Thread.Sleep(500);
                    this.messageThread.Abort();
                }
            }
            catch (Exception e)
            {
                ExceptionHelper.Handle(e);
            }
        }
        #endregion

        #region private methods
        private void messagesChanged(object sender, NotifyCollectionChangedEventArgs e)
        { this.OnPropertyChanged("importantMessages"); this.OnPropertyChanged(); }

        private void createTeil()
        {
            try
            {
                CreateTeilView cv = new CreateTeilView();
                cv.ShowDialog();
                fillView();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Handle(ex);
            }
        }

        private void orderTeil()
        {
            try
            {
                if (string.IsNullOrEmpty(PartToOrder))
                {
                    throw new Exception("Nothing selected to order!");
                }

                BestellenView bv = new BestellenView(allTeile.ToList().Find(item => item.Bezeichnung == PartToOrder),WERKSTATT);
                bv.ShowDialog();
                if ( this.importantMessages.DefaultIfEmpty(null).FirstOrDefault(item => item.teil.Equals(PartToOrder)) != null )
                {
                    this.TeilOk(PartToOrder);
                    this.importantMessages.Remove(this.importantMessages.DefaultIfEmpty(null).FirstOrDefault(item => item.teil.Equals(PartToOrder)));
                    if ( this.importantMessages.Count == 0 )
                        this.timer.Stop();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.Handle(ex);
            }
        }

        private void getMessages()
        {
            try
            {
                while (!shutDownThread)
                {
                   

                    if (this.MainWindow != null)
                    {
                       this.MainWindow.Dispatcher.Invoke(()=> {
                           this.importantMessages.Clear();
                          
                           foreach(Autoteile a in TeileManager.GetKritischeTeile(WERKSTATT,MINBESTAND))
                           {
                               Message m = new Message() { Short = "Lagerbestand von " + a.Bezeichnung + "  kritisch!\nBestand: " + TeileManager.GetBestand(WERKSTATT , a.Bezeichnung) , teil = a.Bezeichnung };
                               this.importantMessages.Add(m);
                               this.TeilNotOk(a);
                               this.timer.Start();
                           }
                           
                        });
                      
                    }

                    Thread.Sleep(TimeSpan.FromMinutes(threadSlepp));
                }
            }

            catch (Exception ex)
            {
                ExceptionHelper.Handle(ex);
            }
        }
     

        private void propertyChanged(params string[] properties)
        {
            foreach (var prop in properties)
            {
                this.OnPropertyChanged(prop);
            }
            this.OnPropertyChanged();
        }

        private string getBestandForTeil(Autoteile selected)
        {
            try
            {
                return TeileManager.GetBestand(WERKSTATT, selected.Bezeichnung).ToString();
            }
            catch(Exception )
            {
                throw ;
            }
        }

        private void fillView()
        {
            try
            {
                allTeile.Clear();
                foreach(Autoteile a in TeileManager.GetAutoteile())
                {
                    this.allTeile.Add(a);
                }
                 
                this.propertyChanged("allTeile");
            }
            
            catch (Exception ex)
            {
                ExceptionHelper.Handle(ex);
            }

        }

        /*private void createTeile()
        {
            this.stattController = new List<Autoteile>();
            Autoteile reifen = new Autoteile() { Bezeichnung = "Reifen", Preis = 400 };
            Autoteile schraube = new Autoteile() { Bezeichnung = "Schraube12x10", Preis = 1 };
            Autoteile vergaser = new Autoteile() { Bezeichnung = "Vergaser", Preis = 310 };

            stattController.Add(reifen);
            stattController.Add(schraube);
            stattController.Add(vergaser);

        }*/
        #endregion
    }
}
