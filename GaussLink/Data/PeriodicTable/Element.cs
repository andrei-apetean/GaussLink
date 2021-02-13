namespace GaussLink.Data.PeriodicTable
{
    public struct Element
    {
        public int AtomicNumber;
        public string Symbol;
        public byte R;
        public byte G;
        public byte B;
        public Element(int AtomicNumber, string Symbol, byte R, byte G, byte B)
        {
            this.AtomicNumber = AtomicNumber;
            this.Symbol = Symbol;
            this.R = R;
            this.G = G;
            this.B = B;
        }
    }
}
