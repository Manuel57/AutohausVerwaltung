using LagerVerwaltung.Helpers;
using LagerVerwaltung.ViewModel;
using LagerverwaltungBL.Model;
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
        public MainWindow()
        {
            InitializeComponent();  
        }

        private void lvTeile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems.Count > 0)
                {
                    Autoteile selected = ((sender as ListView).SelectedItem as Autoteile);
                    (this.root.DataContext as MainWindowViewModel).SetTeilInTxt(selected);

                }
                else
                {
                    throw (new Exception("nothing selected"));
                }
            }
            catch(Exception ex)
            {
                ExceptionHelper.Handle(ex);
                
            }
        }
        
        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            (this.root.DataContext as MainWindowViewModel).ShutThread();
        }

        private void Window_Initialized( object sender , EventArgs e )
        {
            ( this.root.DataContext as MainWindowViewModel ).MainWindow = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Environment.GetCommandLineArgs()?.Any(item =>
             item != null && (bool)item?.Equals("--configure")) == true)
            {
                SettingsManager.Instance.ShowEditor();
            }
            (this.root.DataContext as MainWindowViewModel).Init();
        }
    }
}
