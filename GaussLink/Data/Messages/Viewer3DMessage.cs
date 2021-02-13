using GaussLink.Models;

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
