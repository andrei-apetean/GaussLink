using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.Messages;
using GaussLink.ViewModels.Themes;
using GaussLink.ViewModels.Windows.FileBrowser;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GaussLink.Views.Windows.FileBrowser
{
    /// <summary>
    /// Interaction logic for FileBrowser.xaml
    /// </summary>
    public partial class FileBrowser : Window
    {
        public FileBrowser()
        {
            InitializeComponent();
            Uri iconUri = new Uri("pack://application:,,,/UI/Images/appIconWhite.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            this.DataContext = new FileBrowserViewModel();
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

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = e.Source as ListBox;
            ObservableCollection<DirectoryItemViewModel> d = new ObservableCollection<DirectoryItemViewModel>();
            foreach (DirectoryItemViewModel item in lb.SelectedItems)
            {
                d.Add(item);
            }
            Messenger.Default.Send(new FileBrowserLvSelectMessage(d));
        }
    }
}
