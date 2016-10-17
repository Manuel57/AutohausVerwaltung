
using PDM.Business.Balc;
using PDM.Business.IBalc;
using PDM.UI.Mapper;
using PDM.Win.Events;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using BlEntity = PDM.Business.Entities;
using UIEntity = PDM.UI.Entities;
using System.Data;
namespace PDM.Win.Views.Product
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : Window, INotifyPropertyChanged
    {
        #region Private Members
        private ObservableCollection<PDM.UI.Common.ComboboxEntityBase<int>> productCategoryCollection;

        private PDM.UI.Common.ComboboxEntityBase<int> productCategorySelectedItem;

        private ObservableCollection<PDM.UI.Common.ComboboxEntityBase<int>> productSubCategoryCollection;

        private PDM.UI.Common.ComboboxEntityBase<int> productSubCategorySelectedItem;

        private ObservableCollection<PDM.UI.Common.ComboboxEntityBase<int>> productModelCollection;

        private PDM.UI.Common.ComboboxEntityBase<int> productModelSelectedItem;

        private UIEntity.ProductEntity product;

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

        public PDM.UI.Common.ComboboxEntityBase<int> ProductCategorySelectedItem
        {
            get
            {
                return this.productCategorySelectedItem;
            }
            set
            {
                this.productCategorySelectedItem = value;
                GetSubCategory();
                RaisePropertyChanged("ProductCategorySelectedItem");
            }
        }


        public ObservableCollection<PDM.UI.Common.ComboboxEntityBase<int>> ProductSubCategoryCollection
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

        public PDM.UI.Common.ComboboxEntityBase<int> ProductSubCategorySelectedItem
        {
            get
            {
                return this.productSubCategorySelectedItem;
            }
            set
            {
                this.productSubCategorySelectedItem = value;
                RaisePropertyChanged("ProductSubCategorySelectedItem");
            }
        }


        public ObservableCollection<PDM.UI.Common.ComboboxEntityBase<int>> ProductModelCollection
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

        public PDM.UI.Common.ComboboxEntityBase<int> ProductModelSelectedItem
        {
            get
            {
                return this.productModelSelectedItem;
            }
            set
            {
                this.productModelSelectedItem = value;
                RaisePropertyChanged("ProductModelSelectedItem");
            }
        }

        public UIEntity.ProductEntity Product
        {
            get
            {
                return this.product;
            }
            set
            {
                this.product = value;
                RaisePropertyChanged("Product");
            }
        }
        #endregion

        #region Constructor
        public Create()
        {
            InitializeComponent();
            Load();
            DataContext = this;
        }

        #endregion

        #region Private Methods
        private void Load()
        {
            Product = new UIEntity.ProductEntity();
            ProductCategoryCollection = new ObservableCollection<PDM.UI.Common.ComboboxEntityBase<int>>();
            ProductModelCollection = new ObservableCollection<PDM.UI.Common.ComboboxEntityBase<int>>();

            IBalcBase<BlEntity.ProductCategoryEntity> contextCategory = new ProductCategoryBalc();


            foreach (var category in contextCategory.GetAll().ToList())
            {
                PDM.UI.Common.ComboboxEntityBase<int> target = new PDM.UI.Common.ComboboxEntityBase<int>();
                target.ID = category.ProductCategoryID;
                target.Name = category.Name;
                ProductCategoryCollection.Add(target);
            }

            IBalcBase<BlEntity.ProductModelEntity> contextModel = new ProductModelBalc();
            foreach (var category in contextModel.GetAll().ToList())
            {
                PDM.UI.Common.ComboboxEntityBase<int> target = new PDM.UI.Common.ComboboxEntityBase<int>();
                target.ID = category.ProductModelID;
                target.Name = category.Name;
                ProductModelCollection.Add(target);
            }
        }

        private byte[] ImageToBiteArray(string fileName)
        {
            byte[] data = null;
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                data = new byte[fs.Length];
                fs.Read(data, 0, System.Convert.ToInt32(fs.Length));
            };
            return data;

        }

        private void GetSubCategory()
        {
            ProductSubCategoryCollection = new ObservableCollection<PDM.UI.Common.ComboboxEntityBase<int>>();
            IBalcBase<BlEntity.ProductSubCategoryEntity> contextSubCategory = new ProductSubCategoryBalc();

            foreach (var category in contextSubCategory.GetAll().Where(x => x.ProductCategoryID == productCategorySelectedItem.ID))
            {
                PDM.UI.Common.ComboboxEntityBase<int> target = new PDM.UI.Common.ComboboxEntityBase<int>();
                target.ID = category.ProductSubCategoryID;
                target.Name = category.Name;
                ProductSubCategoryCollection.Add(target);
            }
        }
        #endregion

        #region Click Events
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                txtImagePath.Text = filename;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            if (!string.IsNullOrEmpty(txtImagePath.Text))
            {
                IBalcBase<BlEntity.PhotoEntity> contextPhoto = new PhotoBalc();
                BlEntity.PhotoEntity source = new BlEntity.PhotoEntity();
                source.ModifiedDate = DateTime.Now;
                source.Name = System.IO.Path.GetFileName(txtImagePath.Text);
                source.Image = ImageToBiteArray(txtImagePath.Text);
                source.PhotoID = contextPhoto.Create(source);
                product.PhotoID = source.PhotoID;
            }
            IBalcBase<BlEntity.ProductEntity> contextProduct = new ProductBalc();
            Product.ProductModelID = ProductModelSelectedItem.ID;
            Product.ProductSubCategoryID = ProductSubCategorySelectedItem.ID;
            Product.ModifiedDate = DateTime.Now;

            BlEntity.ProductEntity target = new BlEntity.ProductEntity();
            ProductMapper.MapUIToBusiness(Product, target);
            result = contextProduct.Create(target);

            if (result > 0)
            {
                CallBackEventHander.RaiseMyCustomEvent(this, new AddEventArgs());
                MessageBox.Show("Product Created successfully", "", MessageBoxButton.OK);
                this.Close();
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
    }
}
