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


namespace PDM.Win.Views.ProductCategory
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : UserControl
    {
        #region Constructor
        public Create()
        {
            InitializeComponent();
            CreateCategoryPanel.Visibility = System.Windows.Visibility.Hidden;
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
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCategory.Text))
            {
                IBalcBase<BlEntity.ProductCategoryEntity> context = new ProductCategoryBalc();
                UIEntity.ProductCategoryEntity source = new UIEntity.ProductCategoryEntity();
                source.Name = txtCategory.Text.ToString();
                source.ModifiedDate = DateTime.Now;
                BlEntity.ProductCategoryEntity target = new BlEntity.ProductCategoryEntity();
                ProductCategoryMapper.MapUIToBusiness(source, target);
                int result = context.Create(target);
                if (result > 0)
                {
                    CallBackEventHander.RaiseMyCustomEvent(this, new AddEventArgs());
                    txtCategory.Text = string.Empty;
                }
            }

        }
        #endregion

        #region Methods
        private void Show()
        {
            if (CreateCategoryPanel.Visibility == System.Windows.Visibility.Visible)
            {
                CreateCategoryPanel.Visibility = System.Windows.Visibility.Hidden;
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
            Storyboard.SetTarget(a, CreateCategoryPanel);
            Storyboard.SetTargetProperty(a, new PropertyPath(OpacityProperty));
            storyboard.Completed += delegate { CreateCategoryPanel.Visibility = System.Windows.Visibility.Visible; };
            storyboard.Begin();
        }
        #endregion
    }
}
