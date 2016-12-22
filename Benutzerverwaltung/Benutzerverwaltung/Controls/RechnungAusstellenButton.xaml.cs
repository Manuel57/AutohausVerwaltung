// <copyright file="Benutzerverwaltung.Controls.RechnungAusstellenButton.xaml.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-12-22</date>
// </copyright>

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Benutzerverwaltung.Controls
{
    /// <summary>
    /// Interaction logic for RechnungAusstellenButton.xaml
    /// </summary>
    public partial class RechnungAusstellenButton : UserControl
    {
        public event RoutedEventHandler Click;

        public RechnungAusstellenButton( )
        {
            InitializeComponent();
        }
        public static DependencyProperty Rechnungsnummer = DependencyProperty.Register("RechnungsNummer",typeof(int),typeof(RechnungAusstellenButton));
        public int RechnungsNummer
        {
            get { return ( int ) GetValue(Rechnungsnummer); }
            set { SetValue(Rechnungsnummer , value); }
        }
        public static DependencyProperty CustomerID = DependencyProperty.Register("CustomerId",typeof(int),typeof(RechnungAusstellenButton));
        public int CustomerId
        {
            get { return ( int ) GetValue(CustomerID); }
            set { SetValue(CustomerID , value); }
        }
        private void button_Click( object sender , RoutedEventArgs e )
        {

            if ( null != Click )

                Click(this , e);

        }
    }
}
