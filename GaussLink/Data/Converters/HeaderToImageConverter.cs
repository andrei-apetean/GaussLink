using GaussLink.Data.DirStruct;
using GaussLink.ViewModels.Themes;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace GaussLink.Data.Converters
{
    [ValueConversion(typeof(DirectoryItemType), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (ThemesController.CurrentTheme)
            {
                case ThemeType.ColourfulDark:
                case ThemeType.Dark:
                    value += "_light";
                    break;
                case ThemeType.ColourfulLight:
                case ThemeType.Light:
                    value += "_dark";
                    break;
            }
            return new BitmapImage(new Uri($"pack://application:,,,/UI/Images/{value}.png"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
