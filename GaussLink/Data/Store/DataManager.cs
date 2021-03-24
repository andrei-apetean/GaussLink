using GaussLink.Models;
using System.Collections.Generic;

namespace GaussLink.Data.Store
{
    public static class DataManager
    {
        public static JobFile SelectedJobFile { get; set; }
        public static List<VibrationMode> VibrationModes { get; set; } = new List<VibrationMode>();
        public static List<JobFile> JobsToBeSaved { get; set; } = new List<JobFile>();


    }
}
