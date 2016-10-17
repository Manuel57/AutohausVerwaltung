
using PDM.Business.Balc;
using PDM.Business.IBalc;
using PDM.UI.Mapper;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BlEntity = PDM.Business.Entities;
using UIEntity = PDM.UI.Entities;

namespace PDM.Win
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : UserControl, INotifyPropertyChanged
    {
        private PDM.UI.Common.ComboboxEntityBase<string> selectedItem;
        private ObservableCollection<string> productNameCollection;
        private ObservableCollection<PDM.UI.Common.ComboboxEntityBase<string>> cmbCollection;

        public ObservableCollection<string> ProductNameCollection
        {
            get
            { return this.productNameCollection; }
            set
            {
                this.productNameCollection = value;
                RaisePropertyChanged("ProductNameCollection");
            }
        }
        public PDM.UI.Common.ComboboxEntityBase<string> SelectedItem
        {
            get
            { return this.selectedItem; }
            set
            {
                this.selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }
        public Search()
        {
            InitializeComponent();
            Load();
            DataContext = this;
        }

        public void Load()
        {
            IBalcBase<BlEntity.ProductEntity> context = new ProductBalc();

            ProductNameCollection = new ObservableCollection<string>(context.GetAll().Select(x => x.Name).ToList());

        }
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

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(AutoText.Text))
            {
                UIEntity.ProductEntity target = new UIEntity.ProductEntity();
                IBalcBase<BlEntity.ProductEntity> context = new ProductBalc();
                BlEntity.ProductEntity source = new BlEntity.ProductEntity();
                source = context.GetAll().Where(x => x.Name.Contains(AutoText.Text)).FirstOrDefault();
                ProductMapper.MapBusinessToUI(source, target);
                var window = new PDM.Win.Views.Product.Details(target);
                window.ShowDialog();
            }
        }
    }
}
