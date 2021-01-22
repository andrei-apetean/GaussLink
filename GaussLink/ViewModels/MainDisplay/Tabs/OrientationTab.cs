using GaussLink.Models;
using System.Collections.Generic;

namespace GaussLink.ViewModels.MainDisplay.Tabs
{
    public class OrientationTab : TabContent
    {

        public string OrientationType { get; set; }
        public List<Atom> Orientation { get; set; }

        public OrientationTab(string OrientationType, List<Atom> Orientation)
        {
            this.OrientationType = OrientationType;
            this.Orientation = Orientation;
        }

  
        public OrientationTab()
        {

        }
    }
}
