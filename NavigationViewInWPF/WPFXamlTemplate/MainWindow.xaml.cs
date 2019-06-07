using System;
using System.Windows;
using Windows.UI.Xaml;
using Window = System.Windows.Window;
using XamlIslands=Windows.UI.Xaml.Controls;


namespace WPFXamlTemplate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private XamlIslands.NavigationView _navView;

        public MainWindow()
        {
            InitializeComponent();
            MyNavView.ChildChanged += MyNavView_ChildChanged;
            AppSettings.ThemeEvent += AppSettings_ThemeEvent;
        }

        private void AppSettings_ThemeEvent()
        {
            _navView.RequestedTheme = AppSettings.CurrentTheme;
        }

        private void MyNavView_ChildChanged(object sender, EventArgs e)
        {
            if (MyNavView.Child is XamlIslands.NavigationView navigationView)
            {
                _navView = navigationView;
                _navView.RequestedTheme = ElementTheme.Light;
                _navView.IsBackButtonVisible = XamlIslands.NavigationViewBackButtonVisible.Visible;
                _navView.PaneDisplayMode = XamlIslands.NavigationViewPaneDisplayMode.Left;
                _navView.MenuItems.Add(CreateMenu("Home"));
                _navView.MenuItems.Add(CreateMenu("Admin"));
                _navView.ItemInvoked += NavView_ItemInvoked;
            }
        }

        private void NavView_ItemInvoked(XamlIslands.NavigationView sender, XamlIslands.NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {

            }
            else if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                MessageBox.Show(navItemTag);
            }

        }

        private static XamlIslands.NavigationViewItem CreateMenu(string menuName)
        {
            XamlIslands.NavigationViewItem item = new XamlIslands.NavigationViewItem
            {
                Name = menuName, Content = menuName,Tag = menuName
            };
            return item;
        }
    }
}

