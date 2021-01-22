using System.Collections.Generic;

namespace GaussLink.Models
{
    public class Molecule3D
    {
        public List<VibrationMode> VibrationModes { get; set; }
        public MoleculeBond MoleculeBond { get; set; }
        public MoleculeOrientation MoleculeOrientation { get; set; }
        public Molecule3D(MoleculeBond MoleculeBond, MoleculeOrientation MoleculeOrientation)
        {
            this.MoleculeOrientation = MoleculeOrientation;
            this.MoleculeBond = MoleculeBond;
        }
        public Molecule3D(MoleculeBond MoleculeBond, MoleculeOrientation MoleculeOrientation, List<VibrationMode> VibrationModes)
        {
            this.VibrationModes = VibrationModes;
            this.MoleculeOrientation = MoleculeOrientation;
            this.MoleculeBond = MoleculeBond;
        }

        public Molecule3D()
        {

        }
    }
}
