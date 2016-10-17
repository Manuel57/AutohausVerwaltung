

using PDM.Business.Balc;
using PDM.Business.IBalc;
using PDM.UI.Mapper;
using PDM.Win.Events;
using System;
using System.ComponentModel;
using System.Windows;
using BlEntity = PDM.Business.Entities;
using UIEntity = PDM.UI.Entities;
namespace PDM.Win.Views.Product
{
    /// <summary>
    /// Interaction logic for Delete.xaml
    /// </summary>
    public partial class Delete : Window, INotifyPropertyChanged
    {
        #region Members
        public event EventHandler<CallBackEventArgs<int>> ProductEvent;
        private UIEntity.ProductEntity selectedItem;
        #endregion

        #region Constructor
        public Delete(UIEntity.ProductEntity selectedItem)
        {
            InitializeComponent();
            this.selectedItem = selectedItem;
            DataContext = this;
        }
        #endregion

        #region Click Evetns
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            IBalcBase<BlEntity.ProductEntity> context = new ProductBalc();
            BlEntity.ProductEntity target = new BlEntity.ProductEntity();
            ProductMapper.MapUIToBusiness(selectedItem, target);
            int result = context.Delete(selectedItem.ProductID);
            if (result > 0)
            {
                if (this.ProductEvent != null)
                    this.ProductEvent(this, new CallBackEventArgs<int>(selectedItem.ProductID));
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
