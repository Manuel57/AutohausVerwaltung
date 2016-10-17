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

namespace PDM.Win.Views.ProductSubCategory
{
    /// <summary>
    /// Interaction logic for List.xaml
    /// </summary>
    public partial class List : UserControl, INotifyPropertyChanged
    {
        #region Private Members
        private ObservableCollection<UIEntity.ProductSubCategoryEntity> productSubCategoryCollection;
        private UIEntity.ProductSubCategoryEntity selectedItem;
        public event EventHandler<CallBackEventArgs<int>> ProductCategoryEvent;
        private string categoryName;
        public string CategoryName
        {
            get
            {
                return this.categoryName;
            }
            set
            {
                this.categoryName = value;
                this.RaisePropertyChanged("CategoryName");
            }
        }
        #endregion

        #region Public Properties
        public ObservableCollection<UIEntity.ProductSubCategoryEntity> ProductSubCategoryCollection
        {
            get
            {
                return this.productSubCategoryCollection;
            }
            set
            {
                this.productSubCategoryCollection = value;
                this.RaisePropertyChanged("ProductSubCategoryCollection");
            }
        }

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
                IBalcBase<BlEntity.ProductSubCategoryEntity> context = new ProductSubCategoryBalc();
                ProductSubCategoryCollection = new ObservableCollection<UIEntity.ProductSubCategoryEntity>();
                UIEntity.ProductSubCategoryEntity target = new UIEntity.ProductSubCategoryEntity();
                var source = context.GetAll().Where(x => x.Name == (string)sender).FirstOrDefault();

                IBalcBase<BlEntity.ProductCategoryEntity> cateogryContext = new ProductCategoryBalc();
                target.CategoryName = cateogryContext.GetAll().Where(x => x.ProductCategoryID == source.ProductCategoryID).Select(y => y.Name).FirstOrDefault();
                if (source != null)
                {
                    ProductSubCategoryMapper.MapBusinessToUI(source, target);
                }
                ProductSubCategoryCollection.Add(target);
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
        private ObservableCollection<UIEntity.ProductSubCategoryEntity> Collection()
        {
            IBalcBase<BlEntity.ProductSubCategoryEntity> context = new ProductSubCategoryBalc();
            ProductSubCategoryCollection = new ObservableCollection<UIEntity.ProductSubCategoryEntity>();
            IBalcBase<BlEntity.ProductCategoryEntity> cateogryContext = new ProductCategoryBalc();
            
            foreach (var source in context.GetAll())
            {
                UIEntity.ProductSubCategoryEntity target = new UIEntity.ProductSubCategoryEntity();
                target.CategoryName = cateogryContext.GetAll().Where(x => x.ProductCategoryID == source.ProductCategoryID).Select(y => y.Name).FirstOrDefault();
                ProductSubCategoryMapper.MapBusinessToUI(source, target);
                ProductSubCategoryCollection.Add(target);
            }
            return ProductSubCategoryCollection;
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
            var window = new PDM.Win.Views.ProductSubCategory.Delete(SelectedItem);
            ShowDeleteModal();
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            var window = new PDM.Win.Views.ProductSubCategory.Details(SelectedItem);
            window.ShowDialog();
        }

        #endregion

        #region Show Modal
        private void ShowEditModal()
        {
            var window = new PDM.Win.Views.ProductSubCategory.Edit(SelectedItem);
            window.ProductCategoryEvent += (sender, e) =>
            {
                Collection();
            };
            window.ShowDialog();
        }

        private void ShowDeleteModal()
        {
            var window = new PDM.Win.Views.ProductSubCategory.Delete(SelectedItem);
            window.ProductCategoryEvent += (sender, e) =>
            {
                Collection();
            };
            window.ShowDialog();
        }

        #endregion
    }
}
