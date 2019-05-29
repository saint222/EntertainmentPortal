using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EP._15Puzzle.Data.Models;

namespace EP._15Puzzle.Logic.Profiles
{
    public class DeckProfile:Profile
    {
        public DeckProfile()
        {
            CreateMap<DeckDB, Deck>().ReverseMap();
        }
    }
}
