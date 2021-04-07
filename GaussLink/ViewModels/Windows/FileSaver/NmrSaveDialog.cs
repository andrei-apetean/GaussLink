using GaussLink.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussLink.ViewModels.Windows.FileSaver
{
    public class NmrSaveDialog : SaveDialog
    {
        public NmrSaveDialog()
        {

        }
        public NmrSaveDialog(JobFile jobFile)
        {
            this.JobFile = jobFile;
            this.Name = jobFile.JobName;
        }
    }
}
