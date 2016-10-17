using PDM.Business.Balc;
using PDM.Business.IBalc;
using PDM.UI.Mapper;
using PDM.Win.Events;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using BlEntity = PDM.Business.Entities;
using UIEntity = PDM.UI.Entities;

namespace PDM.Win.Views.Product
{
    /// <summary>
    /// Interaction logic for List.xaml
    /// </summary>
    public partial class List : UserControl, INotifyPropertyChanged
    {
        #region Private Members
        private ObservableCollection<UIEntity.ProductEntity> productCollection;
        private UIEntity.ProductEntity selectedItem;

        #endregion

        #region public Properties
        public ObservableCollection<UIEntity.ProductEntity> ProductCollection
        {
            get
            {
                return this.productCollection;
            }
            set
            {
                this.productCollection = value;
                RaisePropertyChanged("ProductCollection");
            }
        }

        public UIEntity.ProductEntity SelectedItem
        {
            get { return this.selectedItem; }
            set
            {
                this.selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }
        #endregion

        #region Constructor
        public List()
        {
            InitializeComponent();
            Load();
            CallBackEventHander.CustomEvent += CreateProduct;
            DataContext = this;
        }
        #endregion

        #region Callback events
        private void CreateProduct(object sender, EventArgs e)
        {
            if (e is AddEventArgs)
            {
                Load();
            }
        }
        #endregion

        #region Private Methods
        private void Load()
        {
            IBalcBase<BlEntity.ProductEntity> contextProduct = new ProductBalc();
            ProductCollection = new ObservableCollection<UIEntity.ProductEntity>();
            foreach (BlEntity.ProductEntity source in contextProduct.GetAll())
            {
                UIEntity.ProductEntity target = new UIEntity.ProductEntity();
                ProductMapper.MapBusinessToUI(source, target);
                ProductCollection.Add(target);
            }            
        }
        #endregion

        #region Click Events
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            ShowDeleteModal();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            ShowEditModal();
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            var window = new PDM.Win.Views.Product.Details(SelectedItem);
            window.ShowDialog();
        }



        #endregion

        #region Modal Methods
        private void ShowEditModal()
        {
            var window = new PDM.Win.Views.Product.Edit(SelectedItem);
            window.ProductEvent += (sender, e) =>
            {
                if (e.CallBack > 0)
                {
                    var result = MessageBox.Show("Product Updated successfully", "", MessageBoxButton.OK);
                    if (result == MessageBoxResult.OK)
                    {
                        Load();
                    }
                }
                else
                {
                    MessageBox.Show("Problem in updating Product, Please contact Administrator", "", MessageBoxButton.OK);
                }

            };
            window.ShowDialog();
        }

        private void ShowDeleteModal()
        {
            var window = new PDM.Win.Views.Product.Delete(SelectedItem);
            window.ProductEvent += (sender, e) =>
            {
                Load();
            };
            window.ShowDialog();
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
