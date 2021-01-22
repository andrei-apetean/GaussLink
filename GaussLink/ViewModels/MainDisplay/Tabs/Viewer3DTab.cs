

using GaussLink.Models;

namespace GaussLink.ViewModels.MainDisplay.Tabs
{
    public class Viewer3DTab:TabContent
    {
        public Molecule3D Molecule3D { get; set; }
        public Viewer3DTab( Molecule3D Molecule3D)
        {
            this.Molecule3D= Molecule3D;
        }

        public Viewer3DTab()
        {

        }

      
    }
}
