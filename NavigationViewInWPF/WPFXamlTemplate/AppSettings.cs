using Windows.UI.Xaml;

namespace WPFXamlTemplate
{
    public delegate void OnThemeUpdate();
    public static class AppSettings
    {
        public static ElementTheme CurrentTheme = ElementTheme.Default;

        public static event OnThemeUpdate ThemeEvent;

        public static void ThemeUpdate()
        {
            ThemeEvent?.Invoke();
        }

    }
}
