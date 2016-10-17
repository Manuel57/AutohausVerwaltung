
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
using System.Windows.Media.Animation;
using BlEntity = PDM.Business.Entities;
using UIEntity = PDM.UI.Entities;
namespace PDM.Win.Views.ProductSubCategory
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : UserControl, INotifyPropertyChanged
    {
        #region Private Members

        private ObservableCollection<PDM.UI.Common.ComboboxEntityBase<int>> productCategoryCollection;

        private PDM.UI.Common.ComboboxEntityBase<int> selectedItem;
        #endregion

        #region Public Properties
        public ObservableCollection<PDM.UI.Common.ComboboxEntityBase<int>> ProductCategoryCollection
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

        public PDM.UI.Common.ComboboxEntityBase<int> SelectedItem
        {
            get
            {
                return this.selectedItem;
            }
            set
            {
                this.selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }
        #endregion

        #region Constructor
        public Create()
        {
            InitializeComponent();
            Load();
            CreateSubCategoryPanel.Visibility = System.Windows.Visibility.Hidden;
            CallBackEventHander.CustomEvent += VisibleSectionCategory;
            DataContext = this;
        }

        private void VisibleSectionCategory(object sender, EventArgs e)
        {
            if (e is PDM.Win.Events.VisibleEventArgs)
            {
                Show();
            }
        }
        #endregion

        #region Private Methods
        private void Load()
        {
            ProductCategoryCollection = new ObservableCollection<PDM.UI.Common.ComboboxEntityBase<int>>();
            IBalcBase<BlEntity.ProductCategoryEntity> context = new ProductCategoryBalc();
            foreach (var item in context.GetAll().ToList())
            {
                PDM.UI.Common.ComboboxEntityBase<int> target = new PDM.UI.Common.ComboboxEntityBase<int>();
                target.ID = item.ProductCategoryID;
                target.Name = item.Name;
                ProductCategoryCollection.Add(target);
            }

        }
        #endregion

        #region Click Events
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProductSubCategory.Text))
            {
                IBalcBase<BlEntity.ProductSubCategoryEntity> context = new ProductSubCategoryBalc();

                UIEntity.ProductSubCategoryEntity source = new UIEntity.ProductSubCategoryEntity();
                source.Name = txtProductSubCategory.Text.ToString();
                source.ModifiedDate = DateTime.Now;
                source.ProductCategoryID = SelectedItem.ID;
                BlEntity.ProductSubCategoryEntity target = new BlEntity.ProductSubCategoryEntity();
                ProductSubCategoryMapper.MapUIToBusiness(source, target);
                int result = context.Create(target);
                if (result > 0)
                {
                    CallBackEventHander.RaiseMyCustomEvent(this, new AddEventArgs());

                }
                txtProductSubCategory.Text.ToString();
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

        #region Animation
        private void Show()
        {
            if (CreateSubCategoryPanel.Visibility == System.Windows.Visibility.Visible)
            {
                CreateSubCategoryPanel.Visibility = System.Windows.Visibility.Hidden;
                return;
            }
            var a = new DoubleAnimation
            {
                From = 0.5,
                To = 0.0,
                FillBehavior = FillBehavior.Stop,
                BeginTime = TimeSpan.FromSeconds(0.1),
                Duration = new Duration(TimeSpan.FromSeconds(0.1))
            };
            var storyboard = new Storyboard();
            storyboard.Children.Add(a);
            Storyboard.SetTarget(a, CreateSubCategoryPanel);
            Storyboard.SetTargetProperty(a, new PropertyPath(OpacityProperty));
            storyboard.Completed += delegate { CreateSubCategoryPanel.Visibility = System.Windows.Visibility.Visible; };
            storyboard.Begin();
        }
        #endregion
    }
}
