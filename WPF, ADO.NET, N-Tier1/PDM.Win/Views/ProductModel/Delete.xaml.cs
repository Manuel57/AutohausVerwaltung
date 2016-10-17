
using PDM.Business.Balc;
using PDM.Business.IBalc;
using PDM.Win.Events;
using System;
using System.ComponentModel;
using System.Windows;
using BlEntity = PDM.Business.Entities;
using UIEntity = PDM.UI.Entities;


namespace PDM.Win.Views.ProductModel
{
    /// <summary>
    /// Interaction logic for Delete.xaml
    /// </summary>
    public partial class Delete : Window, INotifyPropertyChanged
    {
        #region Private Members
        /// <summary>
        /// ProductModel field
        /// </summary>
        private UIEntity.ProductModelEntity selectedItem;
        #endregion

        #region Public Properties
        /// <summary>
        /// ProductModel callback Event
        /// </summary>
        public event EventHandler<CallBackEventArgs<int>> ProductModelEvent;

        /// <summary>
        /// Gets and Sets the ProductModel object
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

        #region  Constructor
        /// <summary>
        /// Delete Constructor
        /// </summary>
        /// <param name="selectedItem"></param>
        public Delete(UIEntity.ProductModelEntity selectedItem)
        {
            InitializeComponent();
            this.SelectedItem = selectedItem;
            DataContext = this;
        }
        #endregion

        #region Click Events
        /// <summary>
        /// Delete Product Model 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            IBalcBase<BlEntity.ProductModelEntity> context = new ProductModelBalc();
            int result = context.Delete(SelectedItem.ProductModelID);
            if (result > 0)
            {
                if (this.ProductModelEvent != null)
                    this.ProductModelEvent(this, new CallBackEventArgs<int>(SelectedItem.ProductModelID));
                this.Close();
            }
        }

        /// <summary>
        /// Close Product Model Window 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
