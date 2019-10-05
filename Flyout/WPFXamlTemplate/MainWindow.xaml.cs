using System;
using Windows.UI.Xaml.Controls;
using Window = System.Windows.Window;
using XamlIslands = Windows.UI.Xaml.Controls;


namespace WPFXamlTemplate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button BtnFlyout;
        public MainWindow()
        {
            InitializeComponent();
            MyFlyout.ChildChanged += MyFlyout_ChildChanged;           
        }        

        private void MyFlyout_ChildChanged(object sender, EventArgs e)
        {
            if (MyFlyout.Child is XamlIslands.Button btnOk)
            {
                BtnFlyout = btnOk;                
                BtnFlyout.Width = 200;
                BtnFlyout.Height = 200;
                BtnFlyout.Flyout = CreateFlyout();
                BtnFlyout.Content = "Click to view Flyout";
            }
        }

        private Flyout CreateFlyout()
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = "Flyout control";

            Flyout flyout = new Flyout();
            flyout.Placement = XamlIslands.Primitives.FlyoutPlacementMode.Bottom;
            flyout.Content = textBlock;            
            return flyout;
        }        
    }      
}

