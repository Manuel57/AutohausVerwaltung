using LagerVerwaltung.Helpers;
using LagerVerwaltung.ViewModel;
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
                    this.txtSelected.Text = selected.Bezeichnung;
                    this.txtPreis.Text = selected.Preis.ToString();
                    this.txtBestand.Text = (this.root.DataContext as MainWindowViewModel).GetBestandForTeil(selected);
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
            (this.root.DataContext as MainWindowViewModel).shutThread();
        }

        private void Window_Initialized( object sender , EventArgs e )
        {
            ( this.root.DataContext as MainWindowViewModel ).Mw = this;
        }
    }
}
