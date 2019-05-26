using System;
using System.Collections.Generic;
using System.Text;
using EP.Sudoku.Data;
using EP.Sudoku.Logic.Models;
using MediatR;



namespace EP.Sudoku.Logic.Queries
{
    public class PostPlayer : IRequest<Player>
    {
    }
}
