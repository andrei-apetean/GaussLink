using GaussLink.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussLink.ViewModels.Windows.FileSaver
{
    public class OptSaveDialog : SaveDialog
    {
        public OptSaveDialog()
        {

        }
        public OptSaveDialog(JobFile jobFile)
        {
            this.JobFile = jobFile;
            this.Name = jobFile.JobName;
        }

        public OptSaveDialog(JobFile JobFile, bool Content, bool AsJob, bool InputOrientation, bool StandardOrientation)
        {
            this.JobFile = JobFile;
            this.Name = JobFile.JobName;
            this.Content = Content;
            this.AsJob = AsJob;
            this.InputOrientation = InputOrientation;
            this.StandardOrientation = StandardOrientation;
        }
        private bool inputOrientation = false;
        public bool InputOrientation
        {
            get { return inputOrientation; }
            set { inputOrientation = value; OnPropertyChanged(nameof(InputOrientation)); }
        }

        private bool standardOrientation = false;
        public bool StandardOrientation
        {
            get { return standardOrientation; }
            set { standardOrientation = value; OnPropertyChanged(nameof(StandardOrientation)); }
        }
    }
}
