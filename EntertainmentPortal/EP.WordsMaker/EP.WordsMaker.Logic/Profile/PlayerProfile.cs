using System;
using System.Collections.Generic;
using System.Text;
using EP.WordsMaker.Data;
using EP.WordsMaker.Logic.Models;
using AutoMapper;

namespace EP.WordsMaker.Logic.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<PlayerDb, Player>().ReverseMap();
        }
    }
}
