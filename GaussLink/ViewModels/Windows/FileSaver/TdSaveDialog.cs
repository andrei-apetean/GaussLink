using GaussLink.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussLink.ViewModels.Windows.FileSaver
{
    public class TdSaveDialog:SaveDialog
    {
        public TdSaveDialog()
        {

        }
        public TdSaveDialog(JobFile jobFile)
        {
            this.JobFile = jobFile;
            this.Name = jobFile.JobName;
        }
        private bool excitationEnergies;
        public bool ExcitationEnergies
        {
            get { return excitationEnergies; }
            set { excitationEnergies = value; OnPropertyChanged(nameof(ExcitationEnergies)); }
        }
    }
}
