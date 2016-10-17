
using System.ComponentModel;
using System.Windows;


using UIEntity = PDM.UI.Entities;

namespace PDM.Win.Views.ProductModel
{
    /// <summary>
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class Details : Window, INotifyPropertyChanged
    {
        #region Private Members
        /// <summary>
        /// ProductModel field
        /// </summary>
        private UIEntity.ProductModelEntity selectedItem;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets and Sets the ProductModel Object
        /// </summary>
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
        /// <summary>
        /// Product Model Details Constructor
        /// </summary>
        /// <param name="selectedItem"></param>
        public Details(UIEntity.ProductModelEntity selectedItem)
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
