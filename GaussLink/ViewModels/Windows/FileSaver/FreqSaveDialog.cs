using GaussLink.Data;
using GaussLink.Models;
using GaussLink.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GaussLink.ViewModels.Windows.FileSaver
{
    public class FreqSaveDialog:SaveDialog
    {

        public FreqSaveDialog()
        {

        }

        public FreqSaveDialog(JobFile jobFile)
        {
            this.JobFile = jobFile;
            this.Name = jobFile.JobName;
        }

        private bool vibrationModes;

        public bool VibrationModes
        {
            get { return vibrationModes; }
            set { vibrationModes = value; OnPropertyChanged(nameof(VibrationModes)); }
        }


    }
}
