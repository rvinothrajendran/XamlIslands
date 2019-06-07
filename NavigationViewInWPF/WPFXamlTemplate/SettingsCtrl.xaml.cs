using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.UI.Xaml;
using RoutedEventArgs = System.Windows.RoutedEventArgs;

namespace WPFXamlTemplate
{
    /// <summary>
    /// Interaction logic for SettingsCtrl.xaml
    /// </summary>
    public partial class SettingsCtrl : UserControl
    {
        public SettingsCtrl()
        {
            InitializeComponent();
        }

        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rdButton)
            {
                switch (rdButton.Tag.ToString())
                {
                    case "Dark":
                        AppSettings.CurrentTheme = ElementTheme.Dark;
                        break;
                    case "light":
                        AppSettings.CurrentTheme = ElementTheme.Light;
                        break;
                }

                AppSettings.ThemeUpdate();
            }
        }
    }
}
