using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussLink.Models
{
    public class MagneticShieldTensor
    {
        int ID { get; set; }
        string Element { get; set; }
        float Isotropic { get; set; }

        public MagneticShieldTensor(int ID, string Element, float Isotropic)
        {
            this.ID = ID;
            this.Element = Element;
            this.Isotropic = Isotropic;
        }
    }
}
