using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.Messages;
using GaussLink.ViewModels.Base;
using GaussLink.Views.Windows.FileBrowser;
using System.Windows.Input;

namespace GaussLink.ViewModels.MainDisplay
{
    public class OpenFileDisplay : DisplayVM
    {

        public ICommand OpenFileCommand => new RelayCommand(OpenFile);

        public void OpenFile()
        {
            FileBrowser fb = new FileBrowser();
            fb.Show();
        }
    }
}
