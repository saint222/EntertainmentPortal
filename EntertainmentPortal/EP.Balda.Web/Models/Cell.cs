namespace EP.Balda.Models
{
    public class Cell
    {
        public char? Letter { get; set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}