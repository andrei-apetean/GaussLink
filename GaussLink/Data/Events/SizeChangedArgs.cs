using System;

namespace GaussLink.Data.Events
{
    public class SizeChangedArgs:EventArgs
    {
        public double Height { get; set; }
        public double Width { get; set; }

        public SizeChangedArgs(double Height, double Width)
        {
            this.Height = Height;
            this.Width = Width;
        }
        public SizeChangedArgs(){}
    }
}
