using System.Collections.Generic;
using System.Text;

namespace GaussLink.Models
{
    public class ExcitationEnergy
    {
        public List<ExcitedState> ExcitedStates { get; set; }

        public ExcitationEnergy(List<ExcitedState> ExcitedStates)
        {
            this.ExcitedStates = ExcitedStates;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ExcitedState e in ExcitedStates)
            {
                sb.Append("Excited State ").Append(e.ID).Append(": ").Append(e.Multiplicity).Append(" ").Append(e.EvEnergy)
                    .Append(" eV ").Append(e.NmEnergy).Append(" nm f=").Append(e.OscillatorStrength).AppendLine().AppendLine();
                foreach(var item in e.HLGaps)
                {
                    sb.Append(item.LUMO).Append(" -> ").Append(item.HOMO).Append("   ").Append(item.EnergyDelta).AppendLine().AppendLine();
                }
            }
            return sb.ToString();
        }
    }
}
