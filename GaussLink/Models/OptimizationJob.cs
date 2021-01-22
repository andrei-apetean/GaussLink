using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussLink.Models
{
    public class OptimizationJob:Job
    {
        public List<Atom> InputOrientation { get; set; }
        public List<Atom> StandardOrientation { get; set; }

        public OptimizationJob()
        {

        }

        public OptimizationJob(List<Atom> InputOrientation, List<Atom> StandardOrientation )
        {
            this.InputOrientation = InputOrientation;
            this.StandardOrientation = StandardOrientation;
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Input Orientation").AppendLine();
            foreach (Atom a in InputOrientation)
            {
                stringBuilder.Append("Center Number: ").Append(a.CenterNumber).Append(" Atomic Number:").Append(a.AtomicNumber).Append(" Atomic Type").
                    Append(a.AtomicType).Append(" Coordinates: X:").Append(a.AtomCoordinates.X)
                    .Append(" Y:").Append(a.AtomCoordinates.Y).Append(" Z:").Append(a.AtomCoordinates.Z).AppendLine();
            }
            stringBuilder.Append("Standard Orientation").AppendLine();
            foreach (Atom a in StandardOrientation)
            {
                stringBuilder.Append("Center Number: ").Append(a.CenterNumber).Append(" Atomic Number:").Append(a.AtomicNumber).Append(" Atomic Type").
                    Append(a.AtomicType).Append(" Coordinates: X:").Append(a.AtomCoordinates.X)
                    .Append(" Y:").Append(a.AtomCoordinates.Y).Append(" Z:").Append(a.AtomCoordinates.Z).AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}
