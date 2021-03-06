﻿using LagerVerwaltung.ViewModel;
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
    /// Interaktionslogik für CreateTeil.xaml
    /// </summary>
    public partial class CreateTeilView : Window
    {
        public CreateTeilView()
        {
            InitializeComponent();
            (this.root.DataContext as CreateTeilViewModel).close = () => { this.Close(); };
        }
    }
}
