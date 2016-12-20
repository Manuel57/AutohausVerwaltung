using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace LagerVerwaltung.View
{
    /// <summary>
    /// Interaction logic for Geomap.xaml
    /// </summary>
    public partial class Geomap : Window
    {
        public Geomap( )
        {
            InitializeComponent();
        }

        private void Window_Initialized( object sender , EventArgs e )
        {
            string path = System.IO.Path.GetFullPath("./../../ScriptAndPages/Map.html");
            MessageBox.Show(path);
            this.browser.Navigate(new Uri( path, UriKind.Absolute));
        }
    }
}
