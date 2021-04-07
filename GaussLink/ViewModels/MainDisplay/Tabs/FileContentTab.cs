using System.Collections.Generic;

namespace GaussLink.ViewModels.MainDisplay.Tabs
{
    public class FileContentTab : TabContent
    {
        public List<string> results;
        private string selectedItem;
        public string SelectedItem
        { 
            get { return selectedItem; }
            set { selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }


        private string search;
        public string Search
        {
            get { return search; }
            set
            {
                search = value;
                OnPropertyChanged(nameof(Search));

            }
        }
        private int notificationHeight;
        public int NotificationHeight
        {
            get { return notificationHeight; }
            set
            {
                notificationHeight = value;
                OnPropertyChanged(nameof(NotificationHeight));
            }
        }


        public List<string> Content { get; set; } = new List<string>();
        public string FileContent { get; set; }
        public FileContentTab(string FileContent)
        {
            this.FileContent = FileContent;
        }
        public FileContentTab()
        {

        }
        public FileContentTab(List<string> Content) 
        {
            this.Content = Content;
        }
    }
}
