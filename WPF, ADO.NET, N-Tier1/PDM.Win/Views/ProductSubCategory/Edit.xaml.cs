﻿
using PDM.Business.Balc;
using PDM.Business.IBalc;
using PDM.UI.Mapper;
using PDM.Win.Events;
using System;
using System.ComponentModel;
using System.Windows;
using BlEntity = PDM.Business.Entities;
using UIEntity = PDM.UI.Entities;

namespace PDM.Win.Views.ProductSubCategory
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window, INotifyPropertyChanged
    {
        #region Members
        public event EventHandler<CallBackEventArgs<int>> ProductCategoryEvent;
        private UIEntity.ProductSubCategoryEntity selectedItem;
        #endregion

        #region Properties
        public UIEntity.ProductSubCategoryEntity SelectedItem
        {
            get
            {
                return this.selectedItem;
            }
            set
            {
                this.selectedItem = value;
                this.RaisePropertyChanged("SelectedItem");
            }
        }
        #endregion

        #region Constructor
        public Edit(UIEntity.ProductSubCategoryEntity SelectedItem)
        {
            InitializeComponent();
            this.SelectedItem = SelectedItem;
            DataContext = this;
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Click Events
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            IBalcBase<BlEntity.ProductSubCategoryEntity> context = new ProductSubCategoryBalc();
            BlEntity.ProductSubCategoryEntity target = new BlEntity.ProductSubCategoryEntity();
            SelectedItem.ModifiedDate = DateTime.Now;
            ProductSubCategoryMapper.MapUIToBusiness(SelectedItem, target);
            int result = context.Update(target);
            if (result > 0)
            {
                if (this.ProductCategoryEvent != null)
                    this.ProductCategoryEvent(this, new CallBackEventArgs<int>(SelectedItem.ProductSubCategoryID));
                this.Close();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
