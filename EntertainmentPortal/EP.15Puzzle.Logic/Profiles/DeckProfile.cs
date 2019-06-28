using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EP._15Puzzle.Data.Models;
using EP._15Puzzle.Logic.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EP._15Puzzle.Logic.Profiles
{
    public class DeckProfile:Profile
    {
        public DeckProfile()
        {
            CreateMap<LogicDeck, DeckDb>().ReverseMap();
            CreateMap<LogicDeck, Deck>();
            CreateMap<DeckDb, Deck>();
            CreateMap<UserDb, User>();
            CreateMap<LogicTile, Tile>();
            CreateMap<TileDb, LogicTile>()
                .ForMember(t => t.NearbyTiles, opt => opt.Ignore());
            CreateMap<RecordDb, Record>()
                .ForMember(r=>r.Name,opt=>opt.MapFrom(c=>c.User.Name))
                .ForMember(r=>r.Country, opt => opt.MapFrom(c => c.User.Country))
                .ForMember(r=>r.Score, opt => opt.MapFrom(c => c.Score));

            

            
        }
    }
}
