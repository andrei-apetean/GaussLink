using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.Messages;
using GaussLink.ViewModels.Base;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Input;

namespace GaussLink.ViewModels.MainDisplay
{
    public class OpenFileDisplay : DisplayVM
    {

        public ICommand OpenFileCommand => new RelayCommand(OpenFile);

        public void OpenFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var fileList = new List<string>();
                foreach (var file in ofd.FileNames)
                {
                    fileList.Add(file);
                }
                Messenger.Default.Send(new FileExOpenFileMessage(fileList));
            }
        }
    }
}
