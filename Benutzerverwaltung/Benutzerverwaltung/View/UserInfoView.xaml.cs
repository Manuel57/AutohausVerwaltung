// <copyright file="Benutzerverwaltung.View.UserInfoView.xaml.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-12</date>
// </copyright>

using Benutzerverwaltung.Helpers;
using Benutzerverwaltung.ViewModel;
using BenutzerverwaltungBL.Model.DataObjects;
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
using Verwaltung.Exception;

namespace Benutzerverwaltung.View
{
    /// <summary>
    /// Interaction logic for UserInfoView.xaml
    /// </summary>
    public partial class UserInfoView : Window
    {
        public UserInfoView( )
        {
            InitializeComponent();
        }
        public UserInfoView(Customer c )
        {
            InitializeComponent();

            try
            {
                UserInfoViewModel uivm = new UserInfoViewModel(c);
                uivm.CloseAction += ( ) => { this.Close(); };
                this.DataContext = uivm;
            }
            catch ( Exception ex )
            {
                ExceptionHelper.Handle(ex);
            }

        }
    }
}
