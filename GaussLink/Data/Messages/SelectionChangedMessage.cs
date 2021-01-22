using GaussLink.Data;
using GaussLink.Models;

namespace GaussLink.ViewModels
{
    internal class SelectionChangedMessage
    {
        public string Selection { get; set; }
        public JobFile JobFile { get; set; }

        public SelectionChangedMessage(){}

        public SelectionChangedMessage(string Selection)
        {
            this.Selection = Selection;
        }
        public SelectionChangedMessage(string Selection, JobFile JobFile)
        {
            this.Selection = Selection;
            this.JobFile = JobFile;
        }
    }
}