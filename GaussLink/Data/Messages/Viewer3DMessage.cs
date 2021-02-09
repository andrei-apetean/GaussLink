using GaussLink.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussLink.Data.Messages
{
    public class Viewer3DMessage
    {
        public Molecule3D Molecule3D { get; set; }
        public Viewer3DMessage(Molecule3D Molecule3D)
        {
            this.Molecule3D = Molecule3D;
        }
    }
}
