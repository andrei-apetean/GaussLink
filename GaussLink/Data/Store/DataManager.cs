using GaussLink.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussLink.Data.Store
{
    public static class DataManager
    {
        public static JobFile SelectedJobFile { get; set; }
        public static List<VibrationMode> VibrationModes { get; set; } = new List<VibrationMode>();
    }
}
