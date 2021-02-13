using System.Collections.Generic;

namespace GaussLink.Models
{
    public class JobFile
    {
        public string JobName;
        public JobType Type;
        public List<string> Content;

        public JobFile()
        {
        }
        public JobFile(string JobName, JobType Type, List<string> Content)
        {
            this.JobName = JobName;
            this.Type = Type;
            this.Content = Content;
        }

    }
}
