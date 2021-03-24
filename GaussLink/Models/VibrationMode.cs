using System.Collections.Generic;
using System.Text;

namespace GaussLink.Models
{
    public class VibrationMode
    {
        public int Mode { get; set; }
        public string QuantumState { get; set; }
        public float Frequencies { get; set; }
        public float RedMasses { get; set; }
        public float FrcConsts { get; set; }
        public float IRInten { get; set; }
        public float RamanActive { get; set; }
        public float DepolarP { get; set; }
        public float DepolarU { get; set; }

        public List<AtomDelta> AtomVibrations { get; set; } = new List<AtomDelta>();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Mode).AppendLine();
            sb.Append("Frequencies -- ").Append(Frequencies).AppendLine();
            sb.Append("Red. Masses -- ").Append(RedMasses).AppendLine();
            sb.Append("Frec Consts -- ").Append(FrcConsts).AppendLine();
            sb.Append("Raman Active -- ").Append(RamanActive).AppendLine();
            sb.Append("Depolar (P) -- ").Append(DepolarP).AppendLine();
            sb.Append("Depolar (U) -- ").Append(DepolarU).AppendLine();
            sb.Append("Atom   AN   X   Y   Z").AppendLine();

            foreach (AtomDelta atomDelta in AtomVibrations)
            {
                sb.Append(atomDelta.Atom).Append("   ").Append(atomDelta.AtomicNumber).Append(" |  ").Append(atomDelta.Delta.X).Append("   ").Append(atomDelta.Delta.Y).Append("   ").Append(atomDelta.Delta.Z).AppendLine();
            }


            sb.AppendLine();
            return sb.ToString();
        }
    }
}
