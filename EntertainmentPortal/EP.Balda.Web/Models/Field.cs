using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Balda.Models
{
    public class Field
    {
        public Cell[,] Cells { get; set; }
        public int Size { get => _size; }
        public List<Cell> UsedCells { get; set; }
        public List<Cell> AvailableCells { get; set; } //cells available to use at present (near filled cells)

        private readonly int _size;

        public Field()
        {
            _size = 5; //to make 5x5
            Cells = new Cell[5, 5]; 

            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    Cells[i, j] = new Cell(i, j);
                }

            }
        }
    }
}
