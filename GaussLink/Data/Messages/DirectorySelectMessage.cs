using GaussLink.ViewModels.Windows.FileBrowser;

namespace GaussLink.Data.Messages
{
    public class DirectorySelectMessage
    {
        public DirectoryItemViewModel DirectoryItemViewModel { get; }
        public DirectorySelectMessage(DirectoryItemViewModel DirectoryItemViewModel)
        {
            this.DirectoryItemViewModel = DirectoryItemViewModel;
        }
    }
}
