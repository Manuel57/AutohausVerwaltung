using System.ComponentModel;
using System.Windows;

namespace PDM.Win
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Members
        private string _selectedTabName;
        #endregion

        #region Properties
        public string SelectedTabName
        {
            get { return _selectedTabName; }
            set
            {
                if (_selectedTabName != value)
                {
                    _selectedTabName = value;
                    RaisePropertyChanged("SelectedTabName");

                    if (SelectedTabName == "SubCategory")
                    {
                        SubCategoryPanel.Children.Clear();
                        LoadSubCategory();
                    }

                    if (SelectedTabName == "Category")
                    {
                        CategoryPanel.Children.Clear();
                        LoadCategory();
                    }
                    if (SelectedTabName == "ProductModel")
                    {
                        ProductModelPanel.Children.Clear();
                        LoadProductModel();
                    }

                    if (SelectedTabName == "Product")
                    {
                        ProductModelPanel.Children.Clear();
                        LoadProduct();
                    }
                }
            }
        }
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            SelectedTabName = "Product";
            DataContext = this;
        }
        #endregion

        #region Methods
        private void LoadSubCategory()
        {
            SubCategoryPanel.Children.Add(new PDM.Win.Search());
            SubCategoryPanel.Children.Add(new PDM.Win.Views.ProductSubCategory.Index());
        }
        private void LoadCategory()
        {
            CategoryPanel.Children.Add(new PDM.Win.Search());
            CategoryPanel.Children.Add(new PDM.Win.Views.ProductCategory.Index());
        }


        private void LoadProductModel()
        {
            ProductModelPanel.Children.Add(new PDM.Win.Search());
            ProductModelPanel.Children.Add(new PDM.Win.Views.ProductModel.Index());
        }

        private void LoadProduct()
        {
            ProductPanel.Children.Clear();
            ProductPanel.Children.Add(new PDM.Win.Search());
            ProductPanel.Children.Add(new PDM.Win.Views.Product.Index());
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
