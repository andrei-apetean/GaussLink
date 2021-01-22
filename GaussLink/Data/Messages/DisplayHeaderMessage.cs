
using GaussLink.ViewModels.MainDisplay.Tabs;

namespace GaussLink.ViewModels.Messages
{
    public class TabHeaderMessage
    {
        public TabHeader TabHeader { get; set; }
        public TabHeaderMessage(TabHeader DisplayHeader)
        {
            this.TabHeader = DisplayHeader;
        }
    }
}
