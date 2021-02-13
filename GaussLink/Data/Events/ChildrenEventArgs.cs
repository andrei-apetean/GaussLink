using System;

namespace GaussLink.Data.Events
{
    public class ChildrenEventArgs : EventArgs
    {
        public int Count { get; set; }
        public ChildrenEventArgs()
        {

        }
        public ChildrenEventArgs(int Count)
        {
            this.Count = Count;
        }
    }
}
