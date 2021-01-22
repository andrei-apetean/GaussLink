
using GaussLink.Models;

namespace GaussLink.ViewModels.MainDisplay.Tabs
{
    public class VibrationModeTab:TabContent
    {
        public VibrationMode VibrationMode { get; set; }

        public VibrationModeTab( VibrationMode VibrationMode)
        {
            this.VibrationMode = VibrationMode;
        }
        public VibrationModeTab()
        {

        }
    }
}
