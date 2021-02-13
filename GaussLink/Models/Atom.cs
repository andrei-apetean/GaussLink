using System.Windows.Media.Media3D;

namespace GaussLink.Models
{
    public class Atom
    {
        public int CenterNumber { get; set; }
        public int AtomicNumber { get; set; }
        public int AtomicType { get; set; }

        public Vector3D AtomCoordinates { get; set; }
        public Atom()
        {

        }

        public Atom(int CenterNumber, int AtomicNumber, int AtomicType, Vector3D AtomCoordinates)
        {
            this.CenterNumber = CenterNumber;
            this.AtomicNumber = AtomicNumber;
            this.AtomicType = AtomicType;
            this.AtomCoordinates = AtomCoordinates;
        }
    }
}
