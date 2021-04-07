using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.Messages;
using GaussLink.ViewModels.Base;
using GaussLink.Views.Windows.FileBrowser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GaussLink.ViewModels.WindowViewModel
{
    public class WindowViewModel : BaseViewModel
    {
        public WindowViewModel()
        {

        }

        public ICommand OpenFileCommand => new RelayCommand(OpenFile);
        private void OpenFile()
        {
            FileBrowserWindow fb = new FileBrowserWindow();
            fb.Show();
        }

        public ICommand SaveSelectedCommand => new RelayCommand(SaveSelected);
        private void SaveSelected()
        {
            Messenger.Default.Send(new SaveMessage("Selected"));
        }

        public ICommand SaveAllCommand => new RelayCommand(SaveAll);
        private void SaveAll()
        {
            Messenger.Default.Send(new SaveMessage("All"));
        }
    }
}
