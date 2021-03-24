using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.Messages;
using GaussLink.Models;
using GaussLink.ViewModels.Themes;
using GaussLink.ViewModels.Windows.FileSaver;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace GaussLink.Views.Windows.FileSaver
{
    /// <summary>
    /// Interaction logic for FileSaver.xaml
    /// </summary>
    public partial class FileSaverWindow : Window
    {
        public FileSaverWindow()
        {
            InitializeComponent();
            Uri iconUri = new Uri("pack://application:,,,/UI/Images/appIconWhite.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            Messenger.Default.Register<ThemeChangedMessage>(this, ThemeChanged);
        }

        private void ThemeChanged(ThemeChangedMessage obj)
        {
            Uri iconUri;
            switch (obj.ThemeType)
            {
                case ThemeType.Dark:
                    iconUri = new Uri("pack://application:,,,/UI/Images/appIconWhite.png", UriKind.RelativeOrAbsolute);
                    this.Icon = BitmapFrame.Create(iconUri);
                    break;
                case ThemeType.ColourfulDark:
                    iconUri = new Uri("pack://application:,,,/UI/Images/appIconWhite.png", UriKind.RelativeOrAbsolute);
                    this.Icon = BitmapFrame.Create(iconUri);
                    break;
                case ThemeType.Light:
                    iconUri = new Uri("pack://application:,,,/UI/Images/appIconBlack.png", UriKind.RelativeOrAbsolute);
                    this.Icon = BitmapFrame.Create(iconUri);
                    break;
                case ThemeType.ColourfulLight:
                    iconUri = new Uri("pack://application:,,,/UI/Images/appIconBlack.png", UriKind.RelativeOrAbsolute);
                    this.Icon = BitmapFrame.Create(iconUri);
                    break;
            }
        }
    }
}
