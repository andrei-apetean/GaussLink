using GaussLink.ViewModels.Themes;
using GaussLink.Views.Windows.FileBrowser;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace GaussLink
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public App CurrentApplication { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            // loads a png icon, not an ico. 
            ThemesController.CurrentTheme = ThemeType.ColourfulDark;
            Uri iconUri = new Uri("pack://application:,,,/UI/Images/appIconWhite.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            FileBrowser fb = new FileBrowser();
            fb.Show();
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            Uri iconUri;
            switch (int.Parse(((MenuItem)sender).Uid))
            {
                case 0:
                    ThemesController.SetTheme(ThemeType.Light);
                    iconUri = new Uri("pack://application:,,,/UI/Images/appIconBlack.png", UriKind.RelativeOrAbsolute);
                    this.Icon = BitmapFrame.Create(iconUri);
                    break;
                case 1:
                    ThemesController.SetTheme(ThemeType.ColourfulLight);
                    iconUri = new Uri("pack://application:,,,/UI/Images/appIconBlack.png", UriKind.RelativeOrAbsolute);
                    this.Icon = BitmapFrame.Create(iconUri);
                    break;
                case 2:
                    ThemesController.SetTheme(ThemeType.Dark);
                    iconUri = new Uri("pack://application:,,,/UI/Images/appIconWhite.png", UriKind.RelativeOrAbsolute);
                    this.Icon = BitmapFrame.Create(iconUri);
                    break;
                case 3:
                    ThemesController.SetTheme(ThemeType.ColourfulDark);
                    iconUri = new Uri("pack://application:,,,/UI/Images/appIconWhite.png", UriKind.RelativeOrAbsolute);
                    this.Icon = BitmapFrame.Create(iconUri);
                    break;
            }
            e.Handled = true;
        }
    }
}
