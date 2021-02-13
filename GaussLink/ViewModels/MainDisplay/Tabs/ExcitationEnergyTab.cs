using GaussLink.Models;

namespace GaussLink.ViewModels.MainDisplay.Tabs
{
    public class ExcitationEnergyTab : TabContent
    {
        public ExcitationEnergy ExcitationEnergy { get; set; }
        public ExcitationEnergyTab(ExcitationEnergy ExcitationEnergy)
        {
            this.ExcitationEnergy = ExcitationEnergy;
        }

        public ExcitationEnergyTab()
        {

        }
    }
}
