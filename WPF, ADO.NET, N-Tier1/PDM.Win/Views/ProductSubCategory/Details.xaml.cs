
using System.ComponentModel;
using System.Windows;
using UIEntity = PDM.UI.Entities;
namespace PDM.Win.Views.ProductSubCategory
{
    /// <summary>
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class Details : Window, INotifyPropertyChanged
    {
        #region Members
        private UIEntity.ProductSubCategoryEntity selectedItem;
        #endregion

        #region Properties
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
        public Details(UIEntity.ProductSubCategoryEntity selectedItem)
        {
            InitializeComponent();
            this.SelectedItem = selectedItem;
            DataContext = this;
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
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
