using System.Collections.Generic;
using System.Text;

namespace GaussLink.Models
{
    public class MoleculeOrientation
    {
        public List<Atom> Atoms { get; set; }
        public MoleculeOrientation(List<Atom> Atoms)
        {
            this.Atoms = Atoms;
        }
        public MoleculeOrientation()
        {

        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("center_number  atomic_number  atomic_type  X  Y  Z").AppendLine();
            foreach (Atom a in Atoms)
            {
                stringBuilder.Append(a.CenterNumber).Append(" ").Append(a.AtomicNumber).Append(" ").
                    Append(a.AtomicType).Append(" ").Append(a.AtomCoordinates.X)
                    .Append(" ").Append(a.AtomCoordinates.Y).Append(" ").Append(a.AtomCoordinates.Z).AppendLine();
            }
            return stringBuilder.ToString();
        }
    }
}
