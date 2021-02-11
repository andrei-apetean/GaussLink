using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussLink.Data.Messages
{
    class GraphSizeChangedMessage
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public GraphSizeChangedMessage(double Width, double Height)
        {
            this.Width = Width;
            this.Height = Height;
        }
    }
}
