using System;

namespace GaussLink.Data.Events
{
    public class SizeChangedArgs : EventArgs
    {
        public float Height { get; set; }
        public float Width { get; set; }

        public SizeChangedArgs(float Height, float Width)
        {
            this.Height = Height;
            this.Width = Width;
        }
        public SizeChangedArgs() { }
    }
}
