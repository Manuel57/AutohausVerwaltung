using PDM.Business.Balc;
using PDM.Business.IBalc;
using PDM.UI.Mapper;

using PDM.Win.Events;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BlEntity = PDM.Business.Entities;
using UIEntity = PDM.UI.Entities;

namespace PDM.Win.Views.ProductModel
{
    /// <summary>
    /// Interaction logic for List.xaml
    /// </summary>
    public partial class List : UserControl, INotifyPropertyChanged
    {
        #region Private Members
        private ObservableCollection<UIEntity.ProductModelEntity> productModelCollection;
        private UIEntity.ProductModelEntity selectedItem;
        #endregion

        #region Public Properties
        public ObservableCollection<UIEntity.ProductModelEntity> ProductModelCollection
        {
            get
            {
                return this.productModelCollection;
            }
            set
            {
                this.productModelCollection = value;
                this.RaisePropertyChanged("ProductModelCollection");
            }
        }

        public UIEntity.ProductModelEntity SelectedItem
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
        public List()
        {
            InitializeComponent();
            Collection();
            CallBackEventHander.CustomEvent += AddCategory;
            CallBackEventHander.CustomEvent += SearchCategory;
            CallBackEventHander.CustomEvent += RefreshCategory;
            DataContext = this;
        }
        #endregion


        #region Callback Events
        private void RefreshCategory(object sender, EventArgs e)
        {
            if (e is PDM.Win.Events.RefreshEventArgs)
            {
                Collection();
            }
        }

        private void SearchCategory(object sender, EventArgs e)
        {
            if (e is SearchEventArgs)
            {
                IBalcBase<BlEntity.ProductModelEntity> context = new ProductModelBalc();
                ProductModelCollection = new ObservableCollection<UIEntity.ProductModelEntity>();
                UIEntity.ProductModelEntity target = new UIEntity.ProductModelEntity();
                var source = context.GetAll().Where(x => x.Name == (string)sender).FirstOrDefault();
                if (source != null)
                {
                    ProductModelMapper.MapBusinessToUI(source, target);
                }
                ProductModelCollection.Add(target);
            }
        }

        void AddCategory(object sender, EventArgs e)
        {
            if (e is AddEventArgs)
            {
                Collection();
            }
        }
        #endregion

        #region Private Methods
        private void Collection()
        {
            ProductModelCollection = new ObservableCollection<UIEntity.ProductModelEntity>();
            IBalcBase<BlEntity.ProductModelEntity> context = new ProductModelBalc();
            foreach (BlEntity.ProductModelEntity source in context.GetAll().ToList())
            {
                UIEntity.ProductModelEntity target = new UIEntity.ProductModelEntity();
                ProductModelMapper.MapBusinessToUI(source, target);
                ProductModelCollection.Add(target);
            }
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

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            ShowEditModal();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var window = new PDM.Win.Views.ProductModel.Delete(SelectedItem);
            ShowDeleteModal();
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            var window = new PDM.Win.Views.ProductModel.Details(SelectedItem);
            window.ShowDialog();
        }

        #endregion

        #region Show Modal
        private void ShowEditModal()
        {
            var window = new PDM.Win.Views.ProductModel.Edit(SelectedItem);
            window.ProductCategoryEvent += (sender, e) =>
            {
                Collection();
            };
            window.ShowDialog();
        }

        private void ShowDeleteModal()
        {
            var window = new PDM.Win.Views.ProductModel.Delete(SelectedItem);
            window.ProductModelEvent += (sender, e) =>
            {
                Collection();
            };
            window.ShowDialog();
        }
        #endregion
    }
}
