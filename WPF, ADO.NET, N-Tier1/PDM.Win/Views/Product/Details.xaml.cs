
using PDM.Business.Balc;
using PDM.Business.IBalc;
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
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class Details : Window, INotifyPropertyChanged
    {
        #region Private Members
        private UIEntity.ProductEntity product;

        private BitmapImage imageSource;

        private string productModel;

        private string productCategory;

        private string productSubCategory;
        #endregion

        #region Public Members
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

        public string ProductModel
        {
            get
            {
                return this.productModel;
            }
            set
            {
                this.productModel = value;
                RaisePropertyChanged("ProductModel");
            }
        }

        public string ProductCategory
        {
            get
            {
                return this.productCategory;
            }
            set
            {
                this.productCategory = value;
                RaisePropertyChanged("ProductCategory");
            }
        }

        public string ProductSubCategory
        {
            get
            {
                return this.productSubCategory;
            }
            set
            {
                this.productSubCategory = value;
                RaisePropertyChanged("ProductSubCategory");
            }
        }
        #endregion

        #region Constructor
        public Details(UIEntity.ProductEntity product)
        {
            InitializeComponent();
            this.Product = product;
            this.Loaded += Details_Loaded;
            this.DataContext = this;
        }

        #endregion

        #region Events
        void Details_Loaded(object sender, RoutedEventArgs e)
        {
            Get();
            GetPhotoByID();
        }
        #endregion

        #region Click Events
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion


        #region private Methods
        private void Get()
        {
            IBalcBase<BlEntity.ProductModelEntity> contextProductModel = new ProductModelBalc();
            ProductModel = contextProductModel.GetAll().Where(x => x.ProductModelID == Product.ProductModelID).Select(y => y.Name).FirstOrDefault();

            IBalcBase<BlEntity.ProductSubCategoryEntity> contextProductSubCategory = new ProductSubCategoryBalc();
            ProductSubCategory = contextProductSubCategory.GetAll().Where(x => x.ProductSubCategoryID == Product.ProductSubCategoryID).Select(y => y.Name).FirstOrDefault();
            var categoryID = contextProductSubCategory.GetAll().Where(x => x.ProductSubCategoryID == Product.ProductSubCategoryID).Select(y => y.ProductCategoryID).FirstOrDefault();

            IBalcBase<BlEntity.ProductCategoryEntity> contextProductCategory = new ProductCategoryBalc();
            ProductCategory = contextProductCategory.GetAll().Where(x => x.ProductCategoryID == categoryID).Select(y => y.Name).FirstOrDefault();

        }

        private void GetPhotoByID()
        {
            BlEntity.PhotoEntity photo = new BlEntity.PhotoEntity();
            IBalcBase<BlEntity.PhotoEntity> contextPhoto = new PhotoBalc();
            photo = contextPhoto.GetAll().Where(x => x.PhotoID == Product.PhotoID).FirstOrDefault();
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
