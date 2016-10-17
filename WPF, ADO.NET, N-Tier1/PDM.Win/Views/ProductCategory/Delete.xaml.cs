using PDM.Business.Balc;
using PDM.Business.IBalc;
using PDM.Win.Events;
using System;
using System.ComponentModel;
using System.Windows;
using BlEntity = PDM.Business.Entities;
using UIEntity = PDM.UI.Entities;

namespace PDM.Win.Views.ProductCategory
{
    /// <summary>
    /// Interaction logic for Delete.xaml
    /// </summary>
    public partial class Delete : Window, INotifyPropertyChanged
    {
        #region  Members

        public event EventHandler<CallBackEventArgs<int>> ProductCategoryEvent;
        private UIEntity.ProductCategoryEntity selectedItem;
        #endregion

        #region Properties
        public UIEntity.ProductCategoryEntity SelectedItem
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
        public Delete(UIEntity.ProductCategoryEntity selectedItem)
        {
            InitializeComponent();
            this.SelectedItem = selectedItem;
            DataContext = this;
        }
        #endregion

        #region Click Events
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            IBalcBase<BlEntity.ProductCategoryEntity> context = new ProductCategoryBalc();
            int result = context.Delete(SelectedItem.ProductCategoryID);
            if (result > 0)
            {
                if (this.ProductCategoryEvent != null)
                    this.ProductCategoryEvent(this, new CallBackEventArgs<int>(SelectedItem.ProductCategoryID));
                this.Close();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
    }
}
