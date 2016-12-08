// <copyright file="Benutzerverwaltung.Helpers.SettingsDialog.xaml.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-12-8</date>
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
using System.Windows.Shapes;
using Verwaltung.Settings;

namespace Benutzerverwaltung.Helpers
{
    /// <summary>
    /// Interaction logic for SettingsDialog.xaml
    /// </summary>
    public partial class SettingsDialog : Window
    {
        public DatabaseSettings Settings { get; set; }
        public SettingsDialog( )
        {
            InitializeComponent();
            DatabaseSettings testGrid = new DatabaseSettings();
            testGrid.Load();
            wpgMyControl.Instance = testGrid;
        }

        private void Window_Closing( object sender , System.ComponentModel.CancelEventArgs e )
        {
            this.DialogResult = true;
            this.Settings= ( DatabaseSettings ) wpgMyControl.Instance;
            this.Settings.Save();
        }
    }
}
