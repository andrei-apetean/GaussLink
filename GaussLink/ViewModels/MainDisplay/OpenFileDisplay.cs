using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.Messages;
using GaussLink.ViewModels.Base;
using System.Windows.Input;

namespace GaussLink.ViewModels.MainDisplay
{
    public class OpenFileDisplay:DisplayVM
    {
        protected virtual void OnOpeningFile()
        {
            Messenger.Default.Send(new OpenFileMessage());
        }
        public ICommand OpenFileCommand => new RelayCommand(OpenFile);

        public void OpenFile()
        {
            OnOpeningFile();
        }
    }
}
