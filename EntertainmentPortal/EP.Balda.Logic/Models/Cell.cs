namespace EP.Balda.Logic.Models
{
    public class Cell
    {
        public char? Letter { get; set; }
        public int X { get; }
        public int Y { get; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}