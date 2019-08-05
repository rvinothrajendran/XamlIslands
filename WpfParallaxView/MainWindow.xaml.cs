using Microsoft.Toolkit.Wpf.UI.XamlHost;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UwpControl = Windows.UI.Xaml.Controls;

namespace WpfParallaxView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UwpControl.Grid myGrid;
        UwpControl.ListView lstView;

        public MainWindow()
        {
            InitializeComponent();            
        }
       
        private void WindowsXamlHost_ChildChanged(object sender, EventArgs e)
        {
            var windowsHost = (WindowsXamlHost)sender;

            var childCtrl = (UwpControl.Grid) windowsHost.Child;

            if(childCtrl != null)
            {
                lstView = new UwpControl.ListView();
                lstView.Name = "lstView";

                UwpControl.ParallaxView parallax = CreateParallaxView();

                CreateListView();
                myGrid = childCtrl;
                myGrid.Children.Add(parallax);
                myGrid.Children.Add(lstView);              

            }
        }

        private UwpControl.ParallaxView CreateParallaxView()
        {
            UwpControl.ParallaxView parallax = new UwpControl.ParallaxView();
            var binding = new Windows.UI.Xaml.Data.Binding
            {
                Source = lstView
            };

            parallax.Child = CreateImage();
            parallax.VerticalShift = 100;
            parallax.SetBinding(UwpControl.ParallaxView.SourceProperty, binding);
            return parallax;
        }

        private UwpControl.Image CreateImage()
        {
            var image = new UwpControl.Image();
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Win10.jpg");
            var uri = new Uri(path, UriKind.RelativeOrAbsolute);
            image.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(uri);
            image.Stretch = Windows.UI.Xaml.Media.Stretch.UniformToFill;
            return image;
        }
        
        private void CreateListView()
        {
            for (int i = 1; i <= 100; i++)
            {
                lstView.Items.Add("Demo " + i.ToString());
            }
        }

    }
}
