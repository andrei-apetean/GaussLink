using GaussLink.ViewModels.Windows.FileBrowser;

namespace GaussLink.Data.Messages
{
    public class FileBrowserTvSelectMessage
    {
        public DirectoryItemViewModel DirectoryItemViewModel { get; }
        public FileBrowserTvSelectMessage(DirectoryItemViewModel DirectoryItemViewModel)
        {
            this.DirectoryItemViewModel = DirectoryItemViewModel;
        }
    }
}
