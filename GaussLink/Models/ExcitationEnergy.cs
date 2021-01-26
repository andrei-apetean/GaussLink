using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
