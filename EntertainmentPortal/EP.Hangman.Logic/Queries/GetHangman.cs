using System;
using System.Collections.Generic;
using System.Text;
using EP.Hagman.Data;
using MediatR;
using EP.Hangman.Logic.Models;
using EP.Hagman.Logic.Interfaces;

namespace EP.Hangman.Logic.Queries
{
    public class GetHangman : IRequest<HangmanTemporaryData>
    {
    }
}
