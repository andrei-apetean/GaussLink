namespace GaussLink.Data.Messages
{
    public class SizeChangedMessage
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public SizeChangedMessage(double Width, double Height)
        {
            this.Width = Width;
            this.Height = Height;
        }
        public SizeChangedMessage()
        {

        }
    }
}
