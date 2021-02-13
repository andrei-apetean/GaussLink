using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.Messages;
using System;
using System.Windows;

namespace GaussLink.ViewModels.Themes
{
    public static class ThemesController
    {


        public static ThemeType CurrentTheme { get; set; }

        private static ResourceDictionary ThemeDictionary
        {
            get { return Application.Current.Resources.MergedDictionaries[0]; }
            set { Application.Current.Resources.MergedDictionaries[0] = value; }
        }

        private static void ChangeTheme(Uri uri)
        {
            ThemeDictionary = new ResourceDictionary() { Source = uri };
        }
        public static void SetTheme(ThemeType theme)
        {
            string themeName = null;
            CurrentTheme = theme;
            switch (theme)
            {
                case ThemeType.Dark:
                    themeName = "DarkTheme";
                    Messenger.Default.Send(new ThemeChangedMessage(ThemeType.Dark));
                    break;
                case ThemeType.Light:
                    themeName = "LightTheme";
                    Messenger.Default.Send(new ThemeChangedMessage(ThemeType.Light));
                    break;
                case ThemeType.ColourfulDark:
                    themeName = "ColourfulDarkTheme";
                    Messenger.Default.Send(new ThemeChangedMessage(ThemeType.ColourfulDark));
                    break;
                case ThemeType.ColourfulLight:
                    themeName = "ColourfulLightTheme";
                    Messenger.Default.Send(new ThemeChangedMessage(ThemeType.ColourfulLight));
                    break;
            }

            try
            {
                if (!string.IsNullOrEmpty(themeName))
                    ChangeTheme(new Uri($"UI/Themes/{themeName}.xaml", UriKind.Relative));
            }
            catch { }
        }
    }
}
