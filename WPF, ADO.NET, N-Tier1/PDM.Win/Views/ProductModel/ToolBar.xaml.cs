using PDM.Win.Events;
using System.Windows;
using System.Windows.Controls;

namespace PDM.Win.Views.ProductModel
{
    /// <summary>
    /// Interaction logic for ToolBar.xaml
    /// </summary>
    public partial class ToolBar : UserControl
    {
        #region Constructor
        public ToolBar()
        {
            InitializeComponent();
        }

        #endregion

        #region Click Events
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SearchCategory.Text))
            {
                CallBackEventHander.RaiseMyCustomEvent(this.SearchCategory.Text, new SearchEventArgs());
            }

            SearchCategory.Text = string.Empty;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            CallBackEventHander.RaiseMyCustomEvent(true, new RefreshEventArgs());
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            CallBackEventHander.RaiseMyCustomEvent(this, new VisibleEventArgs());
        }
        #endregion
    }
}
