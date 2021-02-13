using System.Collections.Generic;

namespace GaussLink.Models
{
    public class ExcitationEnergy
    {
        public List<ExcitedState> ExcitedStates { get; set; }

        public ExcitationEnergy(List<ExcitedState> ExcitedStates)
        {
            this.ExcitedStates = ExcitedStates;
        }
    }
}
