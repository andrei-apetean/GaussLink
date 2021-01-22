using System.Collections.Generic;

namespace GaussLink.Models
{
    public class MoleculeBond
    {
        public List<Bond> Bonds { get; set; } = new List<Bond>();
        public MoleculeBond()
        {

        }
        public MoleculeBond(List<Bond> Bonds)
        {
            this.Bonds = Bonds;
        }
    }
}
