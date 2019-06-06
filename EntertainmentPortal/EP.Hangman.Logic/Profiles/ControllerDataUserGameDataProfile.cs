using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EP.Hangman.Logic.Models;

namespace EP.Hangman.Logic.Profiles
{
    public class ControllerDataUserGameDataProfile : Profile
    {
        public ControllerDataUserGameDataProfile()
        {
            CreateMap<UserGameData, ControllerData>();
        }
    }
}
