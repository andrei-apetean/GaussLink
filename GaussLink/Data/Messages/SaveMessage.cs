using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussLink.Data.Messages
{
    public class SaveMessage
    {
        public string Message { get; set; }
        public SaveMessage(string Message)
        {
            this.Message = Message;
        }
    }
}
