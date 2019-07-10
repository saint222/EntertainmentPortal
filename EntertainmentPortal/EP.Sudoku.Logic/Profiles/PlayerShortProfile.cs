using AutoMapper;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Logic.Profiles
{
    public class PlayerShortProfile : Profile
    {
        public PlayerShortProfile()
        {
            CreateMap<PlayerDb, PlayerShort>()
                .ForMember(dest => dest.Icon, e => e.MapFrom(src => src.IconDb));

            CreateMap<PlayerShort, PlayerDb>()
               .ForMember(dest => dest.IconDb, e => e.MapFrom(src => src.Icon));
        }
    }
}
