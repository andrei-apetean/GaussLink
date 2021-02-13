using GaussLink.ViewModels.Windows.FileBrowser;
using System.Collections.ObjectModel;

namespace GaussLink.Data.Messages
{
    public class FileBrowserLvSelectMessage
    {
        public ObservableCollection<DirectoryItemViewModel> DirectoryItemViewModels { get; }
        public FileBrowserLvSelectMessage(ObservableCollection<DirectoryItemViewModel> DirectoryItemViewModels)
        {
            this.DirectoryItemViewModels = DirectoryItemViewModels;
        }
    }
}
