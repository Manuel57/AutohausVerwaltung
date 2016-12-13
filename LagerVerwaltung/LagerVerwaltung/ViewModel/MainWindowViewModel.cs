﻿using LagerVerwaltung.Helpers;
using LagerVerwaltung.Model;
using LagerVerwaltung.View;
using LagerverwaltungBL.Configuration;
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
        private static double threadSlepp = 1;

        private List<Autoteile> stattController = null;
        private Thread messageThread = null;
        private bool shutDownThread = false;

        public ObservableCollection<Autoteile> allTeile { get; set; }
        //sollt iwi mit an Thread upgedated werden
        public ObservableCollection<Message> importantMessages { get; set; }
        public RelayCommand CreateTeilCommand { get; set; }
        public RelayCommand OrderTeilCommand { get; set; }
        public Window MainWindow;
        public event EventHandler<EventArgs> Kritisch;
        public string PartToOrder { get; set; }
        public string Preis { get; set; }
        public string Bestand { get; set; }

        public MainWindowViewModel()
        {
            this.allTeile = new ObservableCollection<Autoteile>();
            this.importantMessages = new ObservableCollection<Message>();
            this.importantMessages.CollectionChanged += changed;
            this.CreateTeilCommand = new RelayCommand(this.CreateTeil);
            this.OrderTeilCommand = new RelayCommand(this.OrderTeil);
            this.messageThread = new Thread(GetMessages);
            this.Kritisch += test;
            messageThread.Start();

        }

        public void Init()
        {
            try
            {
                //CongifManager.Initialize();
                FillView();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Handle(ex);
            }
        }

        public void SetTeilInTxt(Autoteile a)
        {
            this.PartToOrder = a.Bezeichnung;
            this.Preis = a.Preis.ToString();
            this.Bestand = GetBestandForTeil(a);
            this.propertyChanged("Preis", "PartToOrder", "Bestand");
        }
        private void changed(object sender, NotifyCollectionChangedEventArgs e)
        { this.OnPropertyChanged("importantMessages"); this.OnPropertyChanged(); }

        private void CreateTeil()
        {
            try
            {
                CreateTeilView cv = new CreateTeilView();
                cv.ShowDialog();
                FillView();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Handle(ex);
            }
        }

        private void OrderTeil()
        {
            try
            {
                if (string.IsNullOrEmpty(PartToOrder))
                {
                    throw new Exception("Nothing selected to order!");
                }

                BestellenView bv = new BestellenView(allTeile.ToList().Find(item => item.Bezeichnung == PartToOrder));
                bv.ShowDialog();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Handle(ex);
            }
        }

        private void GetMessages()
        {
            try
            {
                while (!shutDownThread)
                {
                   

                    if (this.MainWindow != null)
                    {
                       this.MainWindow.Dispatcher.Invoke(()=> {
                           this.importantMessages.Clear();
                           MessageBox.Show("IN invoke");
                            //get alle teile wo der lager bestand kritisch is vom controller
                            for (int i = 0; i < 2; i++)
                            {
                                this.importantMessages.Add(new Message() { Short = "Lagerbestand von Reifen  kritisch!\nBestand: " + DateTime.Now.Minute });
                            }

                        });
                        Kritisch(this, null);
                    }

                    Thread.Sleep(TimeSpan.FromMinutes(threadSlepp));
                }
            }

            catch (Exception ex)
            {
                ExceptionHelper.Handle(ex);
            }
        }
        private void test(object s, EventArgs e)
        {
            //this.OnPropertyChanged("importantMessages");
            //this.OnPropertyChanged();
        }

        private void propertyChanged(params string[] properties)
        {
            foreach (var prop in properties)
            {
                this.OnPropertyChanged(prop);
            }
            this.OnPropertyChanged();
        }

        private string GetBestandForTeil(Autoteile selected)
        {
            //controller. getbestand(selected) ....
            return 2000.ToString();
        }

        private void FillView()
        {
            createTeile();
            foreach (Autoteile a in stattController)
            {
                this.allTeile.Add(a);
            }
            this.propertyChanged("allTeile");
        }

        private void createTeile()
        {
            this.stattController = new List<Autoteile>();
            Autoteile reifen = new Autoteile() { Bezeichnung = "Reifen", Preis = 400 };
            Autoteile schraube = new Autoteile() { Bezeichnung = "Schraube12x10", Preis = 1 };
            Autoteile vergaser = new Autoteile() { Bezeichnung = "Vergaser", Preis = 310 };

            stattController.Add(reifen);
            stattController.Add(schraube);
            stattController.Add(vergaser);

        }

        public void shutThread()
        {
            try
            {
                if (this.messageThread.IsAlive)
                {
                    this.shutDownThread = true;
                    // this.messageThread.Abort();
                }
            }
            catch (Exception e)
            {
                ExceptionHelper.Handle(e);
            }
        }
    }
}
