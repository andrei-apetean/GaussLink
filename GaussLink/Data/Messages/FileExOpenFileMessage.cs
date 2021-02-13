using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussLink.Data.Messages
{
    public class FileExOpenFileMessage
    {
        public List<string> FilePaths { get; }
        public FileExOpenFileMessage(List<string> FilePaths)
        {
            this.FilePaths = FilePaths;
        }
    }
}
