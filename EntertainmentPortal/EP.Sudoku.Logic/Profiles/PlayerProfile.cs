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
            CreateMap<PlayerDb, Player>()
                .ForMember(dest => dest.Icon, e => e.MapFrom(src => src.IconDb))
            .ForMember(dest => dest.GameSessions, e => e.MapFrom(src => src.GameSessionsDb));


            CreateMap<Player, PlayerDb>()
               .ForMember(dest => dest.IconDb, e => e.MapFrom(src => src.Icon))
               .ForMember(dest => dest.GameSessionsDb, e => e.MapFrom(src => src.GameSessions));

        }
    }
}
