using AutoMapper;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Logic.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<PlayerDb, Player>().ForMember(dest => dest.Icon, e => e.MapFrom(src => src.IconDb))
                .ReverseMap().ForMember(dest => dest.IconDb, e => e.MapFrom(src => src.Icon));
            //.ForMember(x => x.IsBaseIcon, x => x.Ignore())
        }
    }
}
