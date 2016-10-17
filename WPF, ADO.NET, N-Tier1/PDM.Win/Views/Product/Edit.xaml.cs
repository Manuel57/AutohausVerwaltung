
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
using System.Windows.Media.Imaging;
using BlEntity = PDM.Business.Entities;
using UIEntity = PDM.UI.Entities;

namespace PDM.Win.Views.Product
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window, INotifyPropertyChanged
    {
        #region Private Members
        private UIEntity.ProductEntity product;
        public event EventHandler<CallBackEventArgs<int>> ProductEvent;
        private ObservableCollection<PDM.UI.Common.ComboboxEntityBase<int>> productCategoryCollection;

        private PDM.UI.Common.ComboboxEntityBase<int> productCategorySelectedItem;

        private ObservableCollection<PDM.UI.Common.ComboboxEntityBase<int>> productSubCategoryCollection;

        private PDM.UI.Common.ComboboxEntityBase<int> productSubCategorySelectedItem;

        private ObservableCollection<PDM.UI.Common.ComboboxEntityBase<int>> productModelCollection;

        private PDM.UI.Common.ComboboxEntityBase<int> productModelSelectedItem;
        private int categorySelectedValue;

        private BitmapImage imageSource;
        #endregion

        #region Public Members
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
                GetSubCategoryByCategory();
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
                RaisePropertyChanged(string.Empty);
            }
        }
        public UIEntity.ProductEntity Product
        {
            get
            { return this.product; }
            set
            {
                this.product = value;
                RaisePropertyChanged("Product");
            }
        }

        public int CategorySelectedValue
        {
            get
            { return this.categorySelectedValue; }
            set
            {
                this.categorySelectedValue = value;
                RaisePropertyChanged("CategorySelectedValue");
            }
        }

        public BitmapImage ImageSource
        {
            get
            { return this.imageSource; }
            set
            {
                this.imageSource = value;
                RaisePropertyChanged("ImageSource");
            }
        }
        #endregion

        #region Constructor
        public Edit(UIEntity.ProductEntity product)
        {
            InitializeComponent();
            this.Product = product;
            this.Loaded += Edit_Loaded;
            this.DataContext = this;
        }
        #endregion

        #region Event
        void Edit_Loaded(object sender, RoutedEventArgs e)
        {
            GetProductCategory();
            GetProductModel();
            GetPhotoByID();
            GetProductModelSelectedItem();
            GetCategorySelectedItem();
            GetSubCategorySelectedItem();
        }
        #endregion

        #region Private Methods
        private void GetSubCategorySelectedItem()
        {
            IBalcBase<BlEntity.ProductEntity> context = new ProductBalc();
            var subcategory = context.GetAll().Where(x => x.ProductSubCategoryID == product.ProductSubCategoryID).FirstOrDefault();
            ProductSubCategorySelectedItem = new PDM.UI.Common.ComboboxEntityBase<int>();
            ProductSubCategorySelectedItem.ID = subcategory.ProductSubCategoryID;

        }

        private void GetCategorySelectedItem()
        {
            IBalcBase<BlEntity.ProductSubCategoryEntity> context = new ProductSubCategoryBalc();
            var subcategory = context.GetAll().Where(x => x.ProductSubCategoryID == product.ProductSubCategoryID).FirstOrDefault();

            IBalcBase<BlEntity.ProductCategoryEntity> contextCategory = new ProductCategoryBalc();
            var category = contextCategory.GetAll().Where(x => x.ProductCategoryID == subcategory.ProductCategoryID).FirstOrDefault();
            ProductCategorySelectedItem = new PDM.UI.Common.ComboboxEntityBase<int>();
            ProductCategorySelectedItem.ID = category.ProductCategoryID;
            ProductCategorySelectedItem.Name = category.Name;
            CategorySelectedValue = ProductCategorySelectedItem.ID;

        }

        private void GetProductModelSelectedItem()
        {
            IBalcBase<BlEntity.ProductModelEntity> context = new ProductModelBalc();
            var productModel = context.GetAll().Where(x => x.ProductModelID == product.ProductModelID).FirstOrDefault();
            ProductModelSelectedItem = new PDM.UI.Common.ComboboxEntityBase<int>();
            ProductModelSelectedItem.ID = productModel.ProductModelID;
            ProductModelSelectedItem.Name = productModel.Name;

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

        private void GetSubCategoryByCategory()
        {
            ProductSubCategoryCollection = new ObservableCollection<PDM.UI.Common.ComboboxEntityBase<int>>();
            IBalcBase<BlEntity.ProductSubCategoryEntity> context = new ProductSubCategoryBalc();
            foreach (var category in context.GetAll().Where(x => x.ProductCategoryID == productCategorySelectedItem.ID))
            {
                PDM.UI.Common.ComboboxEntityBase<int> target = new PDM.UI.Common.ComboboxEntityBase<int>();
                target.ID = category.ProductSubCategoryID;
                target.Name = category.Name;
                ProductSubCategoryCollection.Add(target);
            }
        }

        private void GetProductModel()
        {
            IBalcBase<BlEntity.ProductModelEntity> context = new ProductModelBalc();
            ProductModelCollection = new ObservableCollection<PDM.UI.Common.ComboboxEntityBase<int>>();
            foreach (var category in context.GetAll().ToList())
            {
                PDM.UI.Common.ComboboxEntityBase<int> target = new PDM.UI.Common.ComboboxEntityBase<int>();
                target.ID = category.ProductModelID;
                target.Name = category.Name;
                ProductModelCollection.Add(target);
            }

        }

        private void GetProductCategory()
        {
            IBalcBase<BlEntity.ProductCategoryEntity> context = new ProductCategoryBalc();

            ProductCategoryCollection = new ObservableCollection<PDM.UI.Common.ComboboxEntityBase<int>>();
            foreach (var category in context.GetAll().ToList())
            {
                PDM.UI.Common.ComboboxEntityBase<int> target = new PDM.UI.Common.ComboboxEntityBase<int>();
                target.ID = category.ProductCategoryID;
                target.Name = category.Name;
                ProductCategoryCollection.Add(target);
            }

        }

        private void GetPhotoByID()
        {
            IBalcBase<BlEntity.PhotoEntity> context = new PhotoBalc();
            BlEntity.PhotoEntity photo = new BlEntity.PhotoEntity();
            photo = context.GetAll().Where(x => x.PhotoID == Product.PhotoID).FirstOrDefault();
            ImageSource = LoadImage(photo.Image);
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
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
                IBalcBase<BlEntity.PhotoEntity> context = new PhotoBalc();
                BlEntity.PhotoEntity photo = new BlEntity.PhotoEntity();
                photo = context.GetAll().Where(x => x.PhotoID == Product.PhotoID).FirstOrDefault();
                photo.ModifiedDate = DateTime.Now;
                photo.Name = System.IO.Path.GetFileName(txtImagePath.Text);
                photo.Image = ImageToBiteArray(txtImagePath.Text);
                photo.PhotoID = context.Update(photo);
                product.PhotoID = photo.PhotoID;
            }

            product.ProductModelID = ProductModelSelectedItem.ID;
            product.ProductSubCategoryID = ProductSubCategorySelectedItem.ID;
            product.ModifiedDate = DateTime.Now;
            IBalcBase<BlEntity.ProductEntity> contextProduct = new ProductBalc();

            BlEntity.ProductEntity target = new BlEntity.ProductEntity();
            ProductMapper.MapUIToBusiness(Product, target);
            result = contextProduct.Update(target);

            if (result > 0)
            {
                if (this.ProductEvent != null)
                    this.ProductEvent(this, new CallBackEventArgs<int>(result));
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
