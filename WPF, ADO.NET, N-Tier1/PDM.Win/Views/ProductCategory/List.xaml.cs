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

namespace PDM.Win.Views.ProductCategory
{
    /// <summary>
    /// Interaction logic for List.xaml
    /// </summary>
    public partial class List : UserControl, INotifyPropertyChanged
    {
        #region Private Members
        private ObservableCollection<UIEntity.ProductCategoryEntity> productCategoryCollection;
        private UIEntity.ProductCategoryEntity selectedItem;
        #endregion

        #region Public Properties
        public ObservableCollection<UIEntity.ProductCategoryEntity> ProductCategoryCollection
        {
            get
            {
                return this.productCategoryCollection;
            }
            set
            {
                this.productCategoryCollection = value;
                this.RaisePropertyChanged("ProductCategoryCollection");
            }
        }

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
        public List()
        {
            InitializeComponent();
            Collection();
            CallBackEventHander.CustomEvent += AddCategory;
            CallBackEventHander.CustomEvent += SearchCategory;
            CallBackEventHander.CustomEvent += RefreshCategory;
            DataContext = this;
        }

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
                IBalcBase<BlEntity.ProductCategoryEntity> context = new ProductCategoryBalc();
                ProductCategoryCollection = new ObservableCollection<UIEntity.ProductCategoryEntity>();
                UIEntity.ProductCategoryEntity target = new UIEntity.ProductCategoryEntity();
                var source = context.GetAll().Where(x => x.Name == (string)sender).FirstOrDefault();
                if (source != null)
                {
                    ProductCategoryMapper.MapBusinessToUI(source, target);
                }
                ProductCategoryCollection.Add(target);
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
        private ObservableCollection<UIEntity.ProductCategoryEntity> Collection()
        {
            ProductCategoryCollection = new ObservableCollection<UIEntity.ProductCategoryEntity>();
            IBalcBase<BlEntity.ProductCategoryEntity> context = new ProductCategoryBalc();
            foreach (BlEntity.ProductCategoryEntity source in context.GetAll().ToList())
            {
                UIEntity.ProductCategoryEntity target = new UIEntity.ProductCategoryEntity();
                ProductCategoryMapper.MapBusinessToUI(source, target);
                ProductCategoryCollection.Add(target);
            }
            return ProductCategoryCollection;
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
            var window = new PDM.Win.Views.ProductCategory.Delete(SelectedItem);
            ShowDeleteModal();
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            var window = new PDM.Win.Views.ProductCategory.Details(SelectedItem);
            window.ShowDialog();
        }

        #endregion

        #region Show Modal
        private void ShowEditModal()
        {
            var window = new PDM.Win.Views.ProductCategory.Edit(SelectedItem);
            window.ProductCategoryEvent += (sender, e) =>
            {
                Collection();
            };
            window.ShowDialog();
        }

        private void ShowDeleteModal()
        {
            var window = new PDM.Win.Views.ProductCategory.Delete(SelectedItem);
            window.ProductCategoryEvent += (sender, e) =>
            {
                Collection();
            };
            window.ShowDialog();
        }
        #endregion
    }
}
