using System;
using System.Collections.Generic;
using AutoMapper;
using System.Text;
using EP.DotsBoxes.Data.Models;
using EP.DotsBoxes.Logic.Models;

namespace EP.DotsBoxes.Logic.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<PlayerDb, Player>().ReverseMap();
        }
    }
}
