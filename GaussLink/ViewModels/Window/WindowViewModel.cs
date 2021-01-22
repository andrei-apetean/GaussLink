using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.Messages;
using GaussLink.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GaussLink.ViewModels.Window
{
    public class WindowViewModel:BaseViewModel
    {
        public ICommand OpenFileCommand => new RelayCommand(OpenFile);
        private void OpenFile()
        {
            Messenger.Default.Send(new OpenFileMessage());
        }
    }
}
