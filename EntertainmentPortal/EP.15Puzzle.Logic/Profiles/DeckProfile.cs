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
            CreateMap<RecordDb, Record>()
                .ForMember(r=>r.Name,opt=>opt.MapFrom(c=>c.User.Name))
                .ForMember(r=>r.Country, opt => opt.MapFrom(c => c.User.Country))
                .ForMember(r=>r.Score, opt => opt.MapFrom(c => c.Score));
        }
    }
}
