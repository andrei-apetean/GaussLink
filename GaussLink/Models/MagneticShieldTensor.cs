using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussLink.Models
{
    public class MagneticShieldTensor
    {
        public int ID { get; set; }
        public string Element { get; set; }
        public float Isotropic { get; set; }

        public MagneticShieldTensor(int ID, string Element, float Isotropic)
        {
            this.ID = ID;
            this.Element = Element;
            this.Isotropic = Isotropic;
        }
    }
}
