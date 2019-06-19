using AutoMapper;
using EP.SeaBattle.Data.Models;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.SeaBattle.Logic.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<PlayerDb, Player>().ReverseMap();
            CreateMap<PlayerDb, AddNewPlayerCommand>().ReverseMap();
            CreateMap<Player, AddNewPlayerCommand>().ReverseMap();
            CreateMap<Player, UpdatePlayerCommand>().ReverseMap();
            CreateMap<PlayerDb, UpdatePlayerCommand>().ReverseMap();
        }
    }
}
