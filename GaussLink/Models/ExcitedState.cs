using System.Collections.Generic;

namespace GaussLink.Models
{
    public class ExcitedState
    {
        public int ID { get; set; }
        public string Multiplicity { get; set; }
        public float EvEnergy { get; set; }
        public float NmEnergy { get; set; }
        public float CmEnergy { get; set; }
        public float OscillatorStrength { get; set; }
        public List<HLGap> HLGaps { get; set; } = new List<HLGap>();

        public ExcitedState(int ID, string Multiplicity, float EvEnergy, float NmEnergy, float CmEnergy, float OscillatorStrength, List<HLGap> HLGaps)
        {
            this.ID = ID;
            this.Multiplicity = Multiplicity;
            this.EvEnergy = EvEnergy;
            this.NmEnergy = NmEnergy;
            this.CmEnergy = CmEnergy;
            this.OscillatorStrength = OscillatorStrength;
            this.HLGaps = HLGaps;
        }

        public ExcitedState()
        {

        }
    }
}
