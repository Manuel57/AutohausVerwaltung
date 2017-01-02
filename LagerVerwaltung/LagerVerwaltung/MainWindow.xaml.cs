using LagerVerwaltung.Helpers;
using LagerVerwaltung.ViewModel;
using LagerverwaltungBL.Configuration;
using LagerverwaltungBL.Controller;
using LagerverwaltungBL.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Verwaltung.Exception;
using Verwaltung.Settings;

namespace LagerVerwaltung
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        class Test { public string Name { get; set; } = "Manuel"; public int Age { get; set; } = 18; }
        public MainWindow( )
        {
            InitializeComponent();
            //List<Test> coll = new List<Test>() { new Test() , new Test() };
            //JsonArrayAttribute arr = new JsonArrayAttribute();
            //string s = string.Format("var coordinates ={0};", JsonConvert.SerializeObject(coll, Formatting.Indented));
            
            //MessageBox.Show(s);
        }

        private void lvTeile_SelectionChanged( object sender , SelectionChangedEventArgs e )
        {
            try
            {
                if ( e.AddedItems.Count > 0 )
                {
                    Autoteile selected = ( ( sender as ListView ).SelectedItem as Autoteile );
                    ( this.root.DataContext as MainWindowViewModel ).SetTeilInTxt(selected);

                }
                else
                {
                    throw ( new Exception("nothing selected") );
                }
            }
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);

            }
        }

        private void OnWindowClosing( object sender , CancelEventArgs e )
        {
            ( this.root.DataContext as MainWindowViewModel ).ShutThread();
        }

        private void Window_Initialized( object sender , EventArgs e )
        {
            ( this.root.DataContext as MainWindowViewModel ).MainWindow = this;
        }

        private void Window_Loaded( object sender , RoutedEventArgs e )
        {
            if ( Environment.GetCommandLineArgs()?.Any(item =>
              item != null && ( bool ) item?.Equals("--configure")) == true )
            {
                SettingsManager.Instance.ShowEditor();
                CongifManager.UpdateSettings(SettingsManager.Instance.GetSettings());
            }
            ( this.root.DataContext as MainWindowViewModel ).Init();
            //MessageBox.Show(SdoManager.GetJsonCoordinates(SdoManager.GetZentrallager()));
            //List<Zentrallager> lager = SdoManager.GetZentrallager();
            //string ret = string.Empty;
            //object[] arr = lager.Where(item => item.Coordinates != null).Select(item => new { Name = item.Standort , coordinates = new { lng = item.Coordinates.X , lat = item.Coordinates.Y } }).ToArray();

            //object[] a = arr.Select(item => "getLongLat(" + JsonConvert.SerializeObject(new { Name = "bla" , coordinates = new { lng = "45,12" , lat = "52,2" } })).ToArray();
            //string s = string.Format("function initMap() <| {0} |>" , string.Join(";" , a));
            //s = s.Replace("<|" , "{").Replace("|>" , "}");

        }
    }
}
