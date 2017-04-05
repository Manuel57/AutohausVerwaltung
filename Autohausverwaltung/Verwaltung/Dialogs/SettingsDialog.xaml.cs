using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Verwaltung.Settings;

namespace Verwaltung.Dialogs
{
    /// <summary>
    /// Interaction logic for SettingsDialog.xaml
    /// </summary>
    public partial class SettingsDialog : Window
    {
        /// <summary>
        /// The settings (soöter vlt collection)
        /// </summary>
        private DatabaseSettings settings;
        /// <summary>
        /// gets or sets the settings
        /// </summary>
        public Settings.DatabaseSettings Settings
        {
            get { return settings; }
            set
            {
                this.settings = value;
                //sets the instance of the property grid
                this.wpgMyControl.Instance = settings; 
            }
        }
        public SettingsDialog( )
        {
            InitializeComponent();
        }

        /// <summary>
        /// assigns the settings of the propertygrid to the settings property when the window is closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing( object sender , System.ComponentModel.CancelEventArgs e )
        {
            this.DialogResult = true;
            this.Settings = ( Settings.DatabaseSettings ) wpgMyControl.Instance;
        }

        private void Grid_Initialized( object sender , EventArgs e )
        {

        }

        private void Window_Initialized( object sender , EventArgs e )
        {
            wpgMyControl.Instance = this.Settings;
        }
    }
}
