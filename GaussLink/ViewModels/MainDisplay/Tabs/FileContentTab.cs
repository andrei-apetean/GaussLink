namespace GaussLink.ViewModels.MainDisplay.Tabs
{
    public class FileContentTab : TabContent
    {

        public string FileContent { get; set; }
        public FileContentTab(string FileContent)
        {
            this.FileContent = FileContent;
        }
        public FileContentTab() { }
    }
}
