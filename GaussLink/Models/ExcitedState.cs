using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussLink.Models
{
    public class ExcitedState
    {
        public int ID { get; set; }
        public string QuantumState { get; set; }
        public float ExcitationEnergy { get; set; }
        public float WaveLength { get; set; }
        public float OscillatorStrength { get; set; }
        public List<HLGap> HLGaps { get; set; } = new List<HLGap>();

        public ExcitedState(int ID, string QuantumState, float ExcitationEnergy, float WaveLength, float OscillatorStrength, List<HLGap> HLGaps)
        {
            this.ID = ID;
            this.QuantumState = QuantumState;
            this.ExcitationEnergy = ExcitationEnergy;
            this.WaveLength = WaveLength;
            this.OscillatorStrength = OscillatorStrength;
            this.HLGaps = HLGaps;
        }

        public ExcitedState()
        {

        }
    }
}
