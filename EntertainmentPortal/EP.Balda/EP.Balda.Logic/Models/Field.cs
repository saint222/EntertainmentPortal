namespace EP.Balda.Logic.Models
{
    public class Field
    {
        public Cell[,] Cells { get; set; }
        public int Size { get; }

        public Field()
        {
            Size = 5; //to make 5x5
            Cells = new Cell[Size, Size];

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++) Cells[i, j] = new Cell(i, j);
            }
        }
    }
}