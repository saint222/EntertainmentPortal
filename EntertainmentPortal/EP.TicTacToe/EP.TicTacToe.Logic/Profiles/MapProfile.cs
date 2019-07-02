﻿using AutoMapper;
using EP.TicTacToe.Data.Models;
using EP.TicTacToe.Logic.Models;

namespace EP.TicTacToe.Logic.Profiles
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<MapDb, Map>()
                .ReverseMap();
        }
    }
}