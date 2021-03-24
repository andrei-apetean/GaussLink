using GaussLink.Models;

namespace GaussLink.ViewModels.MainDisplay.Tabs
{


    public class GraphTab : TabContent
    {
        public ExcitationEnergy ExcitationEnergy;
        public GraphTab(ExcitationEnergy ExcitationEnergy)
        {
            this.ExcitationEnergy = ExcitationEnergy;

        }
    }

}

