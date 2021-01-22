using System.Windows.Media.Media3D;

namespace GaussLink.Models
{
    public class AtomDelta
    {
        public int Atom { get; set; }
        public int AtomicNumber { get; set; }
        public Vector3D Delta { get; set; }

        public AtomDelta(int Atom, int AtomicNumber, Vector3D Delta)
        {
            this.Atom = Atom;
            this.AtomicNumber = AtomicNumber;
            this.Delta = Delta;
        }
    }
}
