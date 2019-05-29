namespace EP.Balda.Logic.Models
{
    /// <summary>
    ///     Cell with a value that can contain a letter
    /// </summary>
    public class Cell
    {
        public readonly int _x;
        public readonly int _y;
        public char _letter;

        public Cell(int x, int y, char letter)
        {
            _x      = x;
            _y      = y;
            _letter = letter;
        }
    }
}