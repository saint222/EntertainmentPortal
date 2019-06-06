using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EP._15Puzzle.Data.Models;
using EP._15Puzzle.Logic.Models;

namespace EP._15Puzzle.Logic.Profiles
{
    public class DeckProfile:Profile
    {
        public DeckProfile()
        {
            CreateMap<DeckDb, Deck>().ReverseMap();
            CreateMap<TileDb, Tile>().ReverseMap();
        }
    }
}
