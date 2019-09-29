using System;
using System.Collections.ObjectModel;
using System.Linq;
using Window = System.Windows.Window;
using XamlIslands=Windows.UI.Xaml.Controls;


namespace WPFXamlTemplate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private XamlIslands.AutoSuggestBox _autoSuggestBox;
        private readonly ObservableCollection<string> _dataCollection;

        readonly string[] _fruitNames = new string[] 
            {
                "apple","apricot ","avocado ","banana","blackberry","blackcurrant","blueberry ","camu camu ","cantaloupe ","cherimoya","cherry ",
                "citron ","coconut ","cucumber","cranberry ","date ","fig ","galia","gooseberry","grape ","grapefruit ","guarana"
            };
        public MainWindow()
        {
            InitializeComponent();
            _dataCollection = new ObservableCollection<string>();
            MyAutoBox.ChildChanged += MyNavView_ChildChanged;

        }
        private void MyNavView_ChildChanged(object sender, EventArgs e)
        {
            if (MyAutoBox.Child is XamlIslands.AutoSuggestBox autoSuggestBox)
            {
                _autoSuggestBox = autoSuggestBox;
                _autoSuggestBox.TextChanged += _autoSuggestBox_TextChanged;
                _autoSuggestBox.QuerySubmitted += _autoSuggestBox_QuerySubmitted;
                _autoSuggestBox.SuggestionChosen += _autoSuggestBox_SuggestionChosen;
                _autoSuggestBox.PlaceholderText = "Search";
            }
        }

        private void _autoSuggestBox_SuggestionChosen(XamlIslands.AutoSuggestBox sender, XamlIslands.AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            _autoSuggestBox.Text = "AutoSuggestBox";
        }

        private void _autoSuggestBox_QuerySubmitted(XamlIslands.AutoSuggestBox sender, XamlIslands.AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            _autoSuggestBox.Text = args.ChosenSuggestion != null ? args.ChosenSuggestion.ToString() : sender.Text;
        }
        private void _autoSuggestBox_TextChanged(XamlIslands.AutoSuggestBox sender, XamlIslands.AutoSuggestBoxTextChangedEventArgs args)
        {
            _autoSuggestBox.ItemsSource = _fruitNames.Where(p => p.StartsWith(_autoSuggestBox.Text, StringComparison.OrdinalIgnoreCase)).ToArray(); 
        }
    }
}

