using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussLink.Models
{
    public class HLGap
    {
        public int HOMO { get; set; }
        public int LUMO { get; set; }
        public float EnergyDelta { get; set; }

        public HLGap(int HOMO, int LUMO, float EnergyDelta)
        {
            this.HOMO = HOMO;
            this.LUMO = LUMO;
            this.EnergyDelta = EnergyDelta;
        }
    }
}
