using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.Messages;
using GaussLink.ViewModels.Base;
using System.Collections.Generic;
using System.Windows.Forms;
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
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                var fileList = new List<string>();
                foreach(var file in ofd.FileNames)
                {
                    fileList.Add(file);
                }
                Messenger.Default.Send(new FileExOpenFileMessage(fileList));
            }
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
