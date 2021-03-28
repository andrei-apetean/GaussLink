using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussLink.Models
{
    public class NMR
    {
        public List<MagneticShieldTensor> ShieldingTensors { get; } = new List<MagneticShieldTensor>();

        public NMR(List<MagneticShieldTensor> ShieldingTensors)
        {
            this.ShieldingTensors = ShieldingTensors;
        }

    }
}
