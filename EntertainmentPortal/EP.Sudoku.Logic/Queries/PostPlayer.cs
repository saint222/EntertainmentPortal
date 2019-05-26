using System;
using System.Collections.Generic;
using System.Text;
using EP.Sudoku.Data;
using MediatR;
using EP.Hangman.Sudoku.Models;


namespace EP.Sudoku.Logic.Queries
{
    public class PostPlayer : IRequest<PlayerDb>
    {
    }
}
