using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Windows.UI.Xaml;
using Window = System.Windows.Window;
using XamlIslands = Windows.UI.Xaml.Controls;


namespace WPFXamlTemplate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window , INotifyPropertyChanged
    {
        private XamlIslands.ColorPicker _colorPicker;

        private Color _uwpColorPicker;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public Color uwpColorPicker
        {
            get { return _uwpColorPicker; }
            set
            {
                _uwpColorPicker = value;
                OnPropertyRaised("uwpColorPicker");
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            MyColorView.ChildChanged += MyColorView_ChildChanged;
        }        

        private void MyColorView_ChildChanged(object sender, EventArgs e)
        {
            if (MyColorView.Child is XamlIslands.ColorPicker colorPicker)
            {
                _colorPicker = colorPicker;

                _colorPicker.ColorSpectrumShape = XamlIslands.ColorSpectrumShape.Box;
                _colorPicker.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                _colorPicker.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
                _colorPicker.Margin = new Windows.UI.Xaml.Thickness(10, 10, 10, 10);
                _colorPicker.ColorChanged += ColorPicker_ColorChanged;             
            }
        }

        private void ColorPicker_ColorChanged(XamlIslands.ColorPicker sender, XamlIslands.ColorChangedEventArgs args)
        {
            uwpColorPicker = Color.FromArgb(args.NewColor.A, args.NewColor.R, args.NewColor.G, args.NewColor.B);            
        }        
    }

    public class UWPColorToWPFColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var args = (System.Windows.Media.Color)value;
            var colorUpdate = Color.FromArgb(args.A, args.R, args.G, args.B);
            return new SolidColorBrush(colorUpdate);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

