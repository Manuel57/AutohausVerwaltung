﻿// <copyright file="Benutzerverwaltung.Controls.CustomerButton.xaml.cs">
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
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for CustomerButton.xaml
    /// </summary>
    public partial class CustomerButton : UserControl
    {
        public event RoutedEventHandler Click;
        public int CustomerId { get; set; }
        public CustomerButton( )
        {

            InitializeComponent();

        }

        //public string Text
        //{
        //    get { return ( string ) GetValue(TextProperty); }
        //    set { SetValue(TextProperty , value); }
        //}


        public static readonly DependencyProperty TextProperty =
          DependencyProperty.Register("Text", typeof(string), typeof(CustomerButton), new UIPropertyMetadata(""));

        //public ImageSource Image
        //{
        //    get { return ( ImageSource ) GetValue(ImageProperty); }
        //    set { SetValue(ImageProperty , value); }
        //}

        public static readonly DependencyProperty ImageProperty =
           DependencyProperty.Register("Image", typeof(ImageSource), typeof(CustomerButton), new UIPropertyMetadata(null));

        public static DependencyProperty CommandProperty = DependencyProperty.Register("ButtonVisibility", typeof(ICommand), typeof(CustomerButton));

        public ICommand ButtonVisibility
        {
            get { return ( ICommand ) GetValue(CommandProperty); }
            set { SetValue(CommandProperty , value); }
        }

        public static DependencyProperty CustomerID = DependencyProperty.Register("CustId",typeof(int),typeof(CustomerButton));
        public int CustId
        {
            get { return ( int ) GetValue(CustomerID); }
            set { SetValue(CustomerID , value); }
        }
        private void button_Click( object sender , RoutedEventArgs e )
        {

            if ( null != Click )

                Click(CustId , e);

        }

    }
}
