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
using LagerVerwaltung.Helpers;

namespace LagerVerwaltung.View
{
    /// <summary>
    /// Interaktionslogik für BestellenView.xaml
    /// </summary>
    public partial class BestellenView : Window
    {
        private Autoteile autoteile = new Autoteile() { Bezeichnung ="default",Preis=0};

        public BestellenView()
        {
            InitializeComponent();
        }

        public BestellenView(Autoteile autoteile)
        {
            this.autoteile = autoteile;
            InitializeComponent();
        }
    }
}
