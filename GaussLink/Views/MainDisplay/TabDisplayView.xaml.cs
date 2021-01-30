using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.Messages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
