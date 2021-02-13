using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.Messages;
using System.Windows;
using System.Windows.Controls;

namespace GaussLink.Views.MainDisplay
{
    /// <summary>
    /// Interaction logic for TabDisplayView.xaml
    /// </summary>
    public partial class TabDisplayView : UserControl
    {
        public TabDisplayView()
        {
            InitializeComponent();
            this.Loaded += TabDisplay_Loaded;
            this.SizeChanged += ResizeNotification;
        }

        private void ResizeNotification(object sender, SizeChangedEventArgs e)
        {
            Messenger.Default.Send(new SizeChangedMessage(HeaderPanel.ActualWidth, 0));
        }

        private void TabDisplay_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send(new SizeChangedMessage(HeaderPanel.ActualWidth, 0));
        }
    }
}
