
using PDM.Business.Balc;
using PDM.Business.IBalc;
using PDM.UI.Mapper;
using PDM.Win.Events;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using BlEntity = PDM.Business.Entities;
using UIEntity = PDM.UI.Entities;

namespace PDM.Win.Views.ProductModel
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : UserControl
    {
        #region Constructor
        /// <summary>
        /// Create Constructor
        /// </summary>
        public Create()
        {
            InitializeComponent();
            CreateModelPanel.Visibility = System.Windows.Visibility.Hidden;
            CallBackEventHander.CustomEvent += VisibleSectionCategory;
            DataContext = this;
        }

        #endregion

        #region Events
        private void VisibleSectionCategory(object sender, EventArgs e)
        {
            if (e is PDM.Win.Events.VisibleEventArgs)
            {
                Show();
            }
        }
        #endregion

        #region Click Events
        /// <summary>
        /// Create click Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProductModel.Text))
            {
                IBalcBase<BlEntity.ProductModelEntity> context = new ProductModelBalc();

                UIEntity.ProductModelEntity source = new UIEntity.ProductModelEntity();
                source.Name = txtProductModel.Text.ToString();
                source.ModifiedDate = DateTime.Now;
                BlEntity.ProductModelEntity target = new BlEntity.ProductModelEntity();
                ProductModelMapper.MapUIToBusiness(source, target);
                context.Create(target);

                CallBackEventHander.RaiseMyCustomEvent(this, new AddEventArgs());
            }
            txtProductModel.Text = string.Empty;
        }
        #endregion

        #region Methods
        private void Show()
        {
            if (CreateModelPanel.Visibility == System.Windows.Visibility.Visible)
            {
                CreateModelPanel.Visibility = System.Windows.Visibility.Hidden;
                return;
            }
            var a = new DoubleAnimation
            {
                From = 0.5,
                To = 0.0,
                FillBehavior = FillBehavior.Stop,
                BeginTime = TimeSpan.FromSeconds(0.1),
                Duration = new Duration(TimeSpan.FromSeconds(0.1))
            };
            var storyboard = new Storyboard();
            storyboard.Children.Add(a);
            Storyboard.SetTarget(a, CreateModelPanel);
            Storyboard.SetTargetProperty(a, new PropertyPath(OpacityProperty));
            storyboard.Completed += delegate { CreateModelPanel.Visibility = System.Windows.Visibility.Visible; };
            storyboard.Begin();
        }
        #endregion
    }
}
