using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.Messages;
using GaussLink.ViewModels.Base;
using GaussLink.Views.Windows.FileBrowser;
using System.Windows.Input;

namespace GaussLink.ViewModels.WindowViewModel
{
    public class WindowViewModel : BaseViewModel
    {
        public ICommand OpenFileCommand => new RelayCommand(OpenFile);
        private void OpenFile()
        {
            FileBrowser fb = new FileBrowser();
            fb.Show();
        }
    }
}
