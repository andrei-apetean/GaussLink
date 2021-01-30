using GaussLink.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussLink.ViewModels.MainDisplay.Tabs
{
    public class ExcitationEnergyTab:TabContent
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
