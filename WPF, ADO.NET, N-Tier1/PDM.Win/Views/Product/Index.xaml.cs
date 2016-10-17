using System.Windows;
using System.Windows.Controls;

namespace PDM.Win.Views.Product
{
    /// <summary>
    /// Interaction logic for Index.xaml
    /// </summary>
    public partial class Index : UserControl
    {
        public Index()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            var window = new PDM.Win.Views.Product.Create();
            window.ShowDialog();
        }
    }
}
